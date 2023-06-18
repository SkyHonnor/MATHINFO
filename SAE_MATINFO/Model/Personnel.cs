using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SAE_MATINFO.Model
{

    /// <summary>
    /// Stocke 4 informations :
    /// 1 entier : l'ID du personnel
    /// 3 string : le nom, le prenom et le mail du personnel
    /// </summary> 
    public class Personnel : Crud<Personnel>, ICloneable
    {
        private int idPersonnel;
        private string nomPersonnel;
        private string prenomPersonnel;
        private string mailPersonnel;

        private ObservableCollection<Attribution> attributions;


        /// <summary>
        /// L'IDPersonnel est unique et definit le personnel
        /// </summary>
        public int IdPersonnel
        {
            get
            {
                return this.idPersonnel;
            }

            set
            {
                this.idPersonnel = value;
            }
        }

        /// <summary>
        /// Obtient ou définit le nom du personnel. 
        /// </summary>
        /// <exception cref="ArgumentException"> Envoyée si le nom du personnel n'est pas saisie. 
        public string NomPersonnel
        {
            get
            {
                return this.nomPersonnel;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Le champs NomPersonnel doit etre sasie");

                this.nomPersonnel = value.ToUpper();
            }
        }

        /// <summary>
        /// Obtient ou définit le prenom du personnel. 
        /// </summary>
        /// <exception cref="ArgumentException"> Envoyée si le prenom du personnel n'est pas saisie. 
        public string PrenomPersonnel
        {
            get
            {
                return this.prenomPersonnel;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Le champs PrenomPersonnel doit etre sasie");

                this.prenomPersonnel = value.Substring(0, 1).ToUpper() + value.Substring(1).ToLower();
            }
        }

        /// <summary>
        /// Obtient ou définit le mail du personnel. 
        /// </summary>
        /// <exception cref="ArgumentException"> Envoyée si le mail du personnel n'est pas valide 
        public string MailPersonnel
        {
            get
            {
                return this.mailPersonnel;
            }

            set
            {
                bool valid = MailAddress.TryCreate(value, out _);
                if (!valid)
                    throw new ArgumentException("Le champ mail n'est pas valide");

                this.mailPersonnel = value;
            }
        }

        /// <summary>
        /// Définit une liste d'attributions
        /// </summary>
        public ObservableCollection<Attribution> Attributions
        {
            get
            {
                return this.attributions;
            }

            set
            {
                this.attributions = value;
            }
        }

        public Personnel() {}

        public Personnel(int idPersonnel)
        {
            IdPersonnel = idPersonnel;
        }


        /// <summary>
        /// Constructeur de la classe Personnel.
        /// </summary>
        /// <param name="idPersonnel">L'ID du personnel.</param>
        /// <param name="nomPersonnel">Le nom du personnel.</param>
        /// <param name="prenomPersonnel">Le prenom du personnel.</param>
        /// <param name="mailPersonnel">Le mail du personnel.</param>
        /// <remarks>Ce constructeur initialise les propriétés IdPersonnel, NomPersonnel, PrenomPersonnel et MailPersonnel de l'objet Personnel.</remarks>
        public Personnel(int idPersonnel, string nomPersonnel, string prenomPersonnel, string mailPersonnel)
        {
            IdPersonnel = idPersonnel;
            NomPersonnel = nomPersonnel;
            PrenomPersonnel = prenomPersonnel;
            MailPersonnel = mailPersonnel;

            Attributions = new ObservableCollection<Attribution>();
        }
        public Personnel(string nomPersonnel, string prenomPersonnel, string mailPersonnel)
        : this(0, nomPersonnel, prenomPersonnel, mailPersonnel) { }



        /// <summary>
        /// Permet la creation d'un Personnel dans la base de données.
        /// </summary>
        public void Create()
        {
            DataAccess accesBD = new DataAccess();

            String requete = $"INSERT INTO personnel (nom,prenom,mail) VALUES ('{NomPersonnel}', '{PrenomPersonnel}', '{MailPersonnel}')";

            accesBD.SetData(requete);
            this.Read();
        }


        /// <summary>
        /// Permet la suppression d'un Personnel de la base de données.
        /// </summary>
        public void Delete()
        {
            DataAccess accesBD = new DataAccess();

            String requete = $"DELETE FROM personnel WHERE id_personnel = {IdPersonnel}";

            accesBD.SetData(requete);

        }


        /// <summary>
        /// Permet de chercher le Personnel dans la base de données. 
        /// </summary>
        public void Read()
        {
            DataAccess accesBD = new DataAccess();

            String requetemagique = $"SELECT * FROM personnel WHERE nom = '{NomPersonnel}' AND prenom = '{PrenomPersonnel}'";

            DataTable data = accesBD.GetData(requetemagique);

            if (data != null && data.Rows.Count > 0)
            {
                IdPersonnel = (int)data.Rows[0]["id_personnel"];
                NomPersonnel = (string)data.Rows[0]["nom"];
                PrenomPersonnel = (string)data.Rows[0]["prenom"];
                MailPersonnel = (string)data.Rows[0]["mail"];
            }
        }


        /// <summary>
        /// Permet de modifier les champs du Personnel dans la base données grace à son ID.
        /// </summary>
        public void Update()
        {
            DataAccess accesBD = new DataAccess();

            String requete = $"UPDATE personnel SET nom = '{NomPersonnel}', prenom = '{PrenomPersonnel}', mail = '{MailPersonnel}' WHERE id_personnel = {IdPersonnel}";

            accesBD.SetData(requete);
        }


        /// <summary>
        /// Permet de trouver tous les personnels qui ont était crée dans la base de données.
        /// </summary>
        /// <returns>La methode FindAll renvoie une liste contentant tout les personnels crées.</returns>
        public ObservableCollection<Personnel> FindAll()
        {
            ObservableCollection<Personnel> personnels = new ObservableCollection<Personnel>();
            DataAccess accesBD = new DataAccess();

            String requete = $"SELECT * FROM personnel";

            DataTable data = accesBD.GetData(requete);

            if (data != null)
            {
                foreach (DataRow row in data.Rows)
                {
                    Personnel personnel = new Personnel(
                        (int)row["id_personnel"],
                        (string)row["nom"],
                        (string)row["prenom"],
                        (string)row["mail"]
                    );
                    personnels.Add(personnel);
                }
            }
            return personnels;
        }

        /// <summary>
        /// Permet de trouver les personnels avec des criteres specifiques.
        /// </summary>
        /// <param name="criteres">Un des champs representant le personnel</param>
        /// <returns>Renvoie une liste de tous les personnels qui possedent le critere introduit comme parametre</returns>
        public ObservableCollection<Personnel> FindBySelection(string criteres)
        {
            ObservableCollection<Personnel> personnels = new ObservableCollection<Personnel>();
            DataAccess accesBD = new DataAccess();

            String requete = $"SELECT * FROM personnel WHERE {criteres}";

            DataTable data = accesBD.GetData(requete);

            if (data != null)
            {
                foreach (DataRow row in data.Rows)
                {
                    Personnel personnel = new Personnel(
                        (int)row["id_personnel"],
                        (string)row["nom"],
                        (string)row["prenom"],
                        (string)row["mail"]
                    );
                    personnels.Add(personnel);
                }
            }

            return personnels;
        }


        /// <summary>
        /// Indique si l'objet actuel est égal à un autre objet du même type.
        /// </summary>
        /// <param name="obj">Objet à comparer à cet objet..</param>
        /// <returns>
        ///   <c>true</c> si l'objet spécifié est égal à l'objet actuel ; sinon, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj)
        {
            return obj is Personnel personnel &&
                   this.IdPersonnel == personnel.IdPersonnel &&
                   this.NomPersonnel == personnel.NomPersonnel &&
                   this.PrenomPersonnel == personnel.PrenomPersonnel &&
                   this.MailPersonnel == personnel.MailPersonnel;
        }

        /// <summary>
        /// Indique si deux objets sont égaux.
        /// </summary>
        /// <param name="left">Le premier objet à comparer.</param>
        /// <param name="right">Le deuxième objet à comparer.</param>
        /// <returns>
        ///   <c>true</c> si les objets sont égaux ; sinon, <c>false</c>.
        public static bool operator ==(Personnel? left, Personnel? right)
        {
            return EqualityComparer<Personnel>.Default.Equals(left, right);
        }

        /// <summary>
        /// Indique si deux objets ne sont pas égaux.
        /// </summary>
        /// <param name="left">Le premier objet à comparer.</param>
        /// <param name="right">Le deuxième objet à comparer.</param>
        /// <returns>
        ///   <c>true</c> si les objets ne sont pas égaux ; sinon, <c>false</c>.
        /// </returns>   
        public static bool operator !=(Personnel? left, Personnel? right)
        {
            return !(left == right);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
