using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_MATINFO.Model
{
    public class Materiel : Crud<Materiel>
    {
        public int IdMateriel { get; set; }
        public int FKIdCategorie { get; set; }

        public string NomMateriel { get; set; }
        public string CodeBarre { get; set; }
        public string ReferenceConstructeur { get; set; }

        public Categorie Categorie { get; set; }

        public Materiel() { }

        public Materiel(int idMateriel)
        {
            IdMateriel = idMateriel;
        }

        public Materiel(int idMateriel, int fkIdCategorie, string nomMateriel, string codeBarre, string referenceConstructeur)
        {
            IdMateriel = idMateriel;
            FKIdCategorie = fkIdCategorie;

            NomMateriel = nomMateriel;
            CodeBarre = codeBarre;
            ReferenceConstructeur = referenceConstructeur;
        }

        public Materiel(int fkIdCategorie, string nomMateriel, string codeBarre, string referenceConstructeur) 
        : this(0, fkIdCategorie, nomMateriel, codeBarre, referenceConstructeur) {}

        public void Create()
        {
            DataAccess accesBD = new DataAccess();

            String requete = $"INSERT INTO materiel (nom_materiel, id_categorie, code_barre, reference_constructeur) VALUES ({FKIdCategorie}, '{NomMateriel}', '{CodeBarre}', '{ReferenceConstructeur}')";

            accesBD.SetData(requete);
        }

        public void Delete()
        {
            DataAccess accesBD = new DataAccess();

            String requete = $"DELETE FROM materiel WHERE id_materiel = {IdMateriel}";

            accesBD.SetData(requete);
        }

        public void Read()
        {
            DataAccess accesBD = new DataAccess();

            String requete = $"SELECT * FROM materiel WHERE nom_materiel = '{NomMateriel}'";

            DataTable data = accesBD.GetData(requete);

            if (data != null)
            {
                IdMateriel = (int)data.Rows[0]["id_materiel"];
                FKIdCategorie = (int)data.Rows[0]["id_categorie"];

                NomMateriel = (string)data.Rows[0]["nom_materiel"];
                CodeBarre = (string)data.Rows[0]["code_barre"];
                ReferenceConstructeur = (string)data.Rows[0]["reference_constructeur"];
            }
        }

        public void Update()
        {
            DataAccess accesBD = new DataAccess();

            String requete = $"UPDATE materiel SET nom_materiel = '{NomMateriel}', id_categorie = {FKIdCategorie}, code_barre = '{CodeBarre}', reference_constructeur = '{ReferenceConstructeur}' WHERE id_materiel = {IdMateriel}";

            accesBD.SetData(requete);
        }

        public ObservableCollection<Materiel> FindAll()
        {
            ObservableCollection<Materiel> materiels = new ObservableCollection<Materiel>();
            DataAccess accesBD = new DataAccess();

            String requete = $"SELECT * FROM materiel";

            DataTable data = accesBD.GetData(requete);

            if (data != null)
            {
                foreach (DataRow row in data.Rows)
                {
                    Materiel materiel = new Materiel(
                        (int)row["id_materiel"], (int)row["id_categorie"],
                        (string)row["nom_materiel"], (string)row["code_barre"], (string)row["reference_constructeur"]
                    );

                    materiels.Add(materiel);
                }
            }

            return materiels;
        }

        public ObservableCollection<Materiel> FindBySelection(string criteres)
        {
            ObservableCollection<Materiel> materiels = new ObservableCollection<Materiel>();
            DataAccess accesBD = new DataAccess();

            String requete = $"SELECT * FROM materiel WHERE {criteres}";

            DataTable data = accesBD.GetData(requete);

            if (data != null)
            {
                foreach (DataRow row in data.Rows)
                {
                    Materiel materiel = new Materiel(
                        (int)row["id_materiel"], (int)row["id_categorie"],
                        (string)row["nom_materiel"], (string)row["code_barre"], (string)row["reference_constructeur"]
                    );

                    materiels.Add(materiel);
                }
            }

            return materiels;
        }
    }
}
