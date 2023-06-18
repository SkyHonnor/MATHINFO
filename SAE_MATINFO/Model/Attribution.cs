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
        

        private int fkIdPersonnel;
        private int fkIdMateriel;
        private DateTime fkDateAttribution;
        private string commentaire;
        private Personnel personnel;
        private Materiel materiel;

        public int FKIdPersonnel
        {
            get
            {
                return this.fkIdPersonnel;
            }

            private set
            {
                this.fkIdPersonnel = value;
            }
        }

        public int FKIdMateriel
        {
            get
            {
                return this.fkIdMateriel;
            }

            private set
            {
                this.fkIdMateriel = value;
            }
        }

        public DateTime FKDateAttribution
        {
            get
            {
                return this.fkDateAttribution;
            }

            private set
            {
                this.fkDateAttribution = value;
            }
        }

        public string Commentaire
        {
            get
            {
                return this.commentaire;
            }

            set
            {
                this.commentaire = value;
            }
        }

        public Personnel Personnel
        {
            get
            {
                return this.personnel;
            }

            set
            {
                this.personnel = value;
            }
        }

        public Materiel Materiel
        {
            get
            {
                return this.materiel;
            }

            set
            {
                this.materiel = value;
            }
        }

        public Attribution() { }

        public Attribution(int fkIdPersonnel, int fkIdMateriel, DateTime fkDateAttribution, string commentaire)
        {
            FKIdPersonnel= fkIdPersonnel;
            FKIdMateriel = fkIdMateriel;
            FKDateAttribution = fkDateAttribution;
            Commentaire = commentaire;
        }

        public Attribution(int fkIdPersonnel, int fkIdMateriel, DateTime fkDateAttribution) : this(fkIdPersonnel, fkIdMateriel, fkDateAttribution, "") {}

        public void Create()
        {
            DataAccess accesBD = new DataAccess();

            String requete = $"INSERT INTO appartient (id_personnel, id_materiel, date_attribution, commentaire) VALUES ('{FKIdPersonnel}', '{FKIdMateriel}', '{CastDate(FKDateAttribution)}', '{Commentaire}')";

            accesBD.SetData(requete);
            this.Read();
        }

        public void Delete()
        {
            DataAccess accesBD = new DataAccess();

            String requete = $"DELETE FROM appartient WHERE id_personnel = {FKIdPersonnel} and id_materiel = {FKIdMateriel} and date_attribution = '{CastDate(FKDateAttribution)}'";

            accesBD.SetData(requete);
        }

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

        public void Update()
        {
            DataAccess accesBD = new DataAccess();

            String requete = $"UPDATE appartient SET commentaire = '{Commentaire}' WHERE id_personnel = {FKIdPersonnel} and id_materiel = {FKIdMateriel} and date_attribution = '{CastDate(FKDateAttribution)}'";

            accesBD.SetData(requete);
        }

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
