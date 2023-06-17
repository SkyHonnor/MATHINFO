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
    public class Personnel : Crud<Personnel>
    {
        private int idPersonnel;
        private string nomPersonnel;
        private string prenomPersonnel;
        private string mailPersonnel;

        private ObservableCollection<Attribution> attributions;

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

        public Personnel(int idPersonnel, string nomPersonnel, string prenomPersonnel, string mailPersonnel)
        {
            IdPersonnel = idPersonnel;
            NomPersonnel = nomPersonnel;
            PrenomPersonnel = prenomPersonnel;
            MailPersonnel = mailPersonnel;

        }
        public Personnel(string nomPersonnel, string prenomPersonnel, string mailPersonnel)
        : this(0, nomPersonnel, prenomPersonnel, mailPersonnel) { }


        public void Create()
        {
            DataAccess accesBD = new DataAccess();

            String requete = $"INSERT INTO personnel (nom,prenom,mail) VALUES ('{NomPersonnel}', '{PrenomPersonnel}', '{MailPersonnel}')";

            accesBD.SetData(requete);
            this.Read();
        }

        public void Delete()
        {
            DataAccess accesBD = new DataAccess();

            String requete = $"DELETE FROM personnel WHERE id_personnel = {IdPersonnel}";

            accesBD.SetData(requete);

        }

        public void Read()
        {
            DataAccess accesBD = new DataAccess();

            String requetemagique = $"SELECT * FROM personnel WHERE nom = '{NomPersonnel}' AND prenom = '{PrenomPersonnel}'";

            DataTable data = accesBD.GetData(requetemagique);

            if (data != null & data.Rows.Count > 0)
            {
                IdPersonnel = (int)data.Rows[0]["id_personnel"];
                NomPersonnel = (string)data.Rows[0]["nom"];
                PrenomPersonnel = (string)data.Rows[0]["prenom"];
                MailPersonnel = (string)data.Rows[0]["mail"];
            }
        }

        public void Update()
        {
            DataAccess accesBD = new DataAccess();

            String requete = $"UPDATE personnel SET nom = '{NomPersonnel}', prenom = '{PrenomPersonnel}', mail = '{MailPersonnel}' WHERE id_personnel = {IdPersonnel}";

            accesBD.SetData(requete);
        }

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

        public override bool Equals(object? obj)
        {
            return obj is Personnel personnel &&
                   this.IdPersonnel == personnel.IdPersonnel &&
                   this.NomPersonnel == personnel.NomPersonnel &&
                   this.PrenomPersonnel == personnel.PrenomPersonnel &&
                   this.MailPersonnel == personnel.MailPersonnel;
        }

        public static bool operator ==(Personnel? left, Personnel? right)
        {
            return EqualityComparer<Personnel>.Default.Equals(left, right);
        }

        public static bool operator !=(Personnel? left, Personnel? right)
        {
            return !(left == right);
        }
    }
}
