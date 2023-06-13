using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace SAE_MATINFO.Model
{
    public class Categorie : Crud<Categorie>
    {
        public int IdCategorie { get; set; }
        public string NomCategorie { get; set; }

        public Categorie() { }

        public Categorie(int idCategorie, string nomCategorie)
        {
            IdCategorie = idCategorie;
            NomCategorie = nomCategorie;
        }

        public Categorie(string nomCategorie) : this(0, nomCategorie) {}

        public void Create()
        {
            DataAccess accesBD = new DataAccess();

            String requete = $"INSERT INTO materiel (nom_categorie) VALUES ({NomCategorie})";

            accesBD.SetData(requete);
        }

        public void Delete()
        {
            DataAccess accesBD = new DataAccess();

            String requete = $"DELETE FROM materiel WHERE id_categorie = {IdCategorie}";

            accesBD.SetData(requete);
        }

        public void Read()
        {
            DataAccess accesBD = new DataAccess();

            String requete = $"SELECT * FROM categorie WHERE nom_categorie = {NomCategorie}";

            DataTable data = accesBD.GetData(requete);

            if (data != null)
            {
                IdCategorie = (int)data.Rows[0]["id_categorie"];
                NomCategorie = (string)data.Rows[0]["nom_categorie"];
            }
        }

        public void Update()
        {
            DataAccess accesBD = new DataAccess();

            String requete = $"UPDATE categorie SET id_categorie = {IdCategorie}, nom_categorie = {NomCategorie}";

            accesBD.SetData(requete);
        }

        public ObservableCollection<Categorie> FindAll()
        {
            ObservableCollection<Categorie> categories = new ObservableCollection<Categorie>();
            DataAccess accesBD = new DataAccess();

            String requete = $"SELECT * FROM categorie";

            DataTable data = accesBD.GetData(requete);

            if (data != null)
            {
                foreach (DataRow row in data.Rows)
                {
                    Categorie categorie = new Categorie(
                        (int)row["id_categorie"], (string)row["nom_categorie"]
                    );

                    categories.Add(categorie);
                }
            }

            return categories;
        }

        public ObservableCollection<Categorie> FindBySelection(string criteres)
        {
            ObservableCollection<Categorie> categories = new ObservableCollection<Categorie>();
            DataAccess accesBD = new DataAccess();

            String requete = $"SELECT * FROM categorie WHERE {criteres}";

            DataTable data = accesBD.GetData(requete);

            if (data != null)
            {
                foreach (DataRow row in data.Rows)
                {
                    Categorie categorie = new Categorie(
                        (int)row["id_categorie"], (string)row["nom_categorie"]
                    );

                    categories.Add(categorie);
                }
            }

            return categories;
        }
    }
}
