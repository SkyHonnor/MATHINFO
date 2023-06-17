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

        public ObservableCollection<Materiel> Materiels { get; set; }

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

            String requete = $"INSERT INTO categorie_materiel (nom_categorie) VALUES ('{NomCategorie}')";

            accesBD.SetData(requete);
            this.Read();
        }

        public void Delete()
        {
            DataAccess accesBD = new DataAccess();

            String requete = $"DELETE FROM categorie_materiel WHERE id_categorie = {IdCategorie}";

            accesBD.SetData(requete);
        }

        public void Read()
        {
            DataAccess accesBD = new DataAccess();

            String requete = $"SELECT * FROM categorie_materiel WHERE nom_categorie = '{NomCategorie}'";

            DataTable data = accesBD.GetData(requete);

            if (data != null && data.Rows.Count > 0)
            {
                IdCategorie = (int)data.Rows[0]["id_categorie"];
                NomCategorie = (string)data.Rows[0]["nom_categorie"];
            }
        }

        public void Update()
        {
            DataAccess accesBD = new DataAccess();

            String requete = $"UPDATE categorie_materiel SET id_categorie = {IdCategorie}, nom_categorie = '{NomCategorie}' WHERE id_categorie = {IdCategorie}";

            accesBD.SetData(requete);
        }

        public ObservableCollection<Categorie> FindAll()
        {
            ObservableCollection<Categorie> categories = new ObservableCollection<Categorie>();
            DataAccess accesBD = new DataAccess();

            String requete = $"SELECT * FROM categorie_materiel";

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

            String requete = $"SELECT * FROM categorie_materiel WHERE {criteres}";

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

        public override bool Equals(object? obj)
        {
            return obj is Categorie categorie &&
                   this.IdCategorie == categorie.IdCategorie &&
                   this.NomCategorie == categorie.NomCategorie &&
                   EqualityComparer<ObservableCollection<Materiel>>.Default.Equals(this.Materiels, categorie.Materiels);
        }

        public static bool operator ==(Categorie? left, Categorie? right)
        {
            return EqualityComparer<Categorie>.Default.Equals(left, right);
        }

        public static bool operator !=(Categorie? left, Categorie? right)
        {
            return !(left == right);
        }
    }
}
