using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_MATINFO.Model
{
    public class Personnel : Crud<Personnel>
    {
        public int IdPersonnel { get; set; }

        public string NomPersonnel { get; set; }
        public string PrenomPersonnel { get; set; }
        public string MailPersonnel { get; set; }

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

            if (data != null)
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
    }
}
