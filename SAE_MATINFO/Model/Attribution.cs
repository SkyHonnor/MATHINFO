using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Xml;

namespace SAE_MATINFO.Model
{
    public class Attribution : Crud<Attribution>
    {
        /// <summary>
        /// Stocke 6 informations :
        /// 2 entiers : la fkIdPersonnel et la fkIdMateriel
        /// 1 datetime : la fkIdMateriel
        /// 1 string : le commentaire de l'attribution
        /// 1 Personnel: un personnel
        /// 1 Materiel: un materiel
        /// </summary> 

        private int fKIdPersonnel;
        private int fKIdMateriel;
        private DateTime fKDateAttribution;

        private string commentaire;

        private Personnel personnel;
        private Materiel materiel;


        /// <summary>
        /// La FKIdPersonnel definit la clé etrangere relié à l'Id Personnel
        /// </summary>
        public int FKIdPersonnel
        {
            get
            {
                return this.fKIdPersonnel;
            }

            private set
            {
                this.fKIdPersonnel = value;
            }
        }

        /// <summary>
        /// La FKIdMateriel definit la clé etrangere relié à l'Id Materiel
        /// </summary>
        public int FKIdMateriel
        {
            get
            {
                return this.fKIdMateriel;
            }

            private set
            {
                this.fKIdMateriel = value;
            }
        }

        /// <summary>
        /// La FKDateAttribution definit la clé etrangere relié à la date attribution
        /// </summary>
        public DateTime FKDateAttribution
        {
            get
            {
                return this.fKDateAttribution;
            }

            set
            {
                this.fKDateAttribution = value;
            }
        }

        /// <summary>
        /// Definit le commentaire de l'attribution
        /// </summary>
        public string Commentaire
        {
            get
            {
                return this.commentaire;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Le champs Commentaire doit etre saisie");

                this.commentaire = value;
            }
        }

        /// <summary>
        /// Définit le Personnel relié à l'attribution
        /// </summary>
        public Personnel Personnel
        {
            get
            {
                return this.personnel;
            }

            set
            {
                this.FKIdPersonnel = value.IdPersonnel;
                this.personnel = value;
            }
        }

        /// <summary>
        /// Définit le Materiel relié à l'attribution
        /// </summary>
        public Materiel Materiel
        {
            get
            {
                return this.materiel;
            }

            set
            {
                this.FKIdMateriel = value.IdMateriel;
                this.materiel = value;
            }
        }
        public Attribution() { }


        /// <summary>
        /// Constructeur de la classe Attribution.
        /// </summary>
        /// <param name="fkIdPersonnel">La fkIdPersonnel de l'attribution.</param>
        /// <param name="fkIdMateriel">La fkIdMateriel de l'attribution.</param>
        /// <param name="fkDateAttribution">La fkDateAttribution de l'attribution..</param>
        /// <param name="commentaire">Le commentaire de l'attribution.</param>
        /// <remarks>Ce constructeur initialise les propriétés fkIdPersonnel, fkIdMateriel, fkDateAttribution et commentaire de l'objet Attribution.</remarks>
        public Attribution(int fkIdPersonnel, int fkIdMateriel, DateTime fkDateAttribution, string commentaire)
        {
            FKIdPersonnel= fkIdPersonnel;
            FKIdMateriel = fkIdMateriel;
            FKDateAttribution = fkDateAttribution;
            Commentaire = commentaire;
        }

        public Attribution(int fkIdPersonnel, int fkIdMateriel, DateTime fkDateAttribution) : this(fkIdPersonnel, fkIdMateriel, fkDateAttribution, "") {}

        /// <summary>
        /// Permet la creation d'une Attribution dans la base de données.
        /// </summary>
        public void Create()
        {
            DataAccess accesBD = new DataAccess();

            String requete = $"INSERT INTO appartient (id_personnel, id_materiel, date_attribution, commentaire) VALUES ('{FKIdPersonnel}', '{FKIdMateriel}', '{CastDate(FKDateAttribution)}', '{Commentaire}')";

            accesBD.SetData(requete);
            this.Read();
        }


        /// <summary>
        /// Permet la suppression d'une Attribution de la base de données.
        /// </summary>
        public void Delete()
        {
            DataAccess accesBD = new DataAccess();

            String requete = $"DELETE FROM appartient WHERE id_personnel = {FKIdPersonnel} and id_materiel = {FKIdMateriel} and date_attribution = '{CastDate(FKDateAttribution)}'";

            accesBD.SetData(requete);
        }

        /// <summary>
        /// Permet de chercher l' Attribution dans la base de données. 
        /// </summary>
        public void Read()
        {
            DataAccess accesBD = new DataAccess();

            String requete = $"SELECT * FROM appartient WHERE id_personnel = {FKIdPersonnel} and id_materiel = {FKIdMateriel} and date_attribution = '{CastDate(FKDateAttribution)}'";

            DataTable data = accesBD.GetData(requete);

            if (data != null && data.Rows.Count > 0)
            {
                FKIdPersonnel = (int)data.Rows[0]["id_personnel"];
                FKIdMateriel = (int)data.Rows[0]["id_materiel"];
                FKDateAttribution = (DateTime)data.Rows[0]["date_attribution"];

                Commentaire = (string)data.Rows[0]["commentaire"];
            }
        }

        /// <summary>
        /// Permet de modifier les champs de l'Attribution dans la base données grace à son ID.
        /// </summary>
        public void Update()
        {
            DataAccess accesBD = new DataAccess();

            String requete = $"UPDATE appartient SET commentaire = '{Commentaire}' WHERE id_personnel = {FKIdPersonnel} and id_materiel = {FKIdMateriel} and date_attribution = '{CastDate(FKDateAttribution)}'";

            accesBD.SetData(requete);
        }


        /// <summary>
        /// Permet de trouver tous les attributions qui ont était crée dans la base de données.
        /// </summary>
        /// <returns>La methode FindAll renvoie une liste contentant tout les attributions crées.</returns>
        public ObservableCollection<Attribution> FindAll()
        {
            ObservableCollection<Attribution> attributions = new ObservableCollection<Attribution>();
            DataAccess accesBD = new DataAccess();

            String requete = $"SELECT * FROM appartient";

            DataTable data = accesBD.GetData(requete);

            if (data != null)
            {
                foreach (DataRow row in data.Rows)
                {
                    Attribution attribution = new Attribution(
                        (int)row["id_personnel"], (int)row["id_materiel"],
                        (DateTime)row["date_attribution"],
                        (string)row["commentaire"]
                    );

                    attributions.Add(attribution);
                }
            }

            return attributions;
        }


        /// <summary>
        /// Permet de trouver les attributions avec des criteres specifiques.
        /// </summary>
        /// <param name="criteres">Un des champs representant l' Attribution</param>
        /// <returns>Renvoie une liste de tous les attributions qui possedent le critere introduit comme parametre</returns>
        public ObservableCollection<Attribution> FindBySelection(string criteres)
        {
            ObservableCollection<Attribution> attributions = new ObservableCollection<Attribution>();
            DataAccess accesBD = new DataAccess();

            String requete = $"SELECT * FROM appartient WHERE {criteres}";

            DataTable data = accesBD.GetData(requete);

            if (data != null)
            {
                foreach (DataRow row in data.Rows)
                {
                    Attribution attribution = new Attribution(
                        (int)row["id_personnel"], (int)row["id_materiel"],
                        (DateTime)row["date_attribution"],
                        (string)row["commentaire"]
                    );

                    attributions.Add(attribution);
                }
            }

            return attributions;
        }

        public override bool Equals(object? obj)
        {
            return obj is Attribution attribution &&
                   this.FKIdPersonnel == attribution.FKIdPersonnel &&
                   this.FKIdMateriel == attribution.FKIdMateriel &&
                   this.FKDateAttribution == attribution.FKDateAttribution &&
                   this.Commentaire == attribution.Commentaire &&
                   EqualityComparer<Personnel>.Default.Equals(this.Personnel, attribution.Personnel) &&
                   EqualityComparer<Materiel>.Default.Equals(this.Materiel, attribution.Materiel);
        }

        public static bool operator ==(Attribution? left, Attribution? right)
        {
            return EqualityComparer<Attribution>.Default.Equals(left, right);
        }

        public static bool operator !=(Attribution? left, Attribution? right)
        {
            return !(left == right);
        }

        private string CastDate(DateTime d)
        {
            return $"{d.Year}/{d.Month}/{d.Day}";
        }
    }
}
