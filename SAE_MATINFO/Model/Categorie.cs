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

    /// <summary>
    /// Stocke 3 informations :
    /// 1 entier : l'ID de la categorie
    /// 1 string : le nom de la categorie
    /// 1 Observable Collection: liste de materiels
    /// </summary> 
    public class Categorie : Crud<Categorie>, ICloneable
    {
        private int idCategorie;
        private string nomCategorie;

        private ObservableCollection<Materiel> materiels = new ObservableCollection<Materiel>();


        /// <summary>
        /// L'IdCategorie est unique et definit la categorie
        /// </summary>
        public int IdCategorie
        {
            get
            {
                return this.idCategorie;
            }

            set
            {
                this.idCategorie = value;
            }
        }

        /// <summary>
        /// Obtient ou définit le nom de la categorie. 
        /// </summary>
        /// <exception cref="ArgumentException"> Envoyée si le nom de la categorie n'est pas saisie. 
        public string NomCategorie
        {
            get
            {
                return this.nomCategorie;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Le champs NomCategorie doit etre sasie");

                this.nomCategorie = value;
            }
        }

        /// <summary>
        /// Définit une liste de materiels
        /// </summary>
        public ObservableCollection<Materiel> Materiels
        {
            get
            {
                return this.materiels;
            }

            set
            {
                this.materiels = value;
            }
        }

        public Categorie() { }

        /// <summary>
        /// Constructeur de la classe Categorie.
        /// </summary>
        /// <param name="idCategorie">L'ID de la categorie</param>
        /// <param name="nomCategorie">Le nom de la cateogie</param>
        public Categorie(int idCategorie, string nomCategorie)
        {
            IdCategorie = idCategorie;
            NomCategorie = nomCategorie;
        }

        public Categorie(string nomCategorie) : this(0, nomCategorie) {}

        /// <summary>
        /// Permet la creation d'une categorie dans la base de données.
        /// </summary>
        public void Create()
        {
            DataAccess accesBD = new DataAccess();

            String requete = $"INSERT INTO categorie_materiel (nom_categorie) VALUES ('{NomCategorie}')";

            accesBD.SetData(requete);
            this.Read();
        }

        /// <summary>
        /// Permet la suppression d'une categorie de la base de données.
        /// </summary>
        public void Delete()
        {
            DataAccess accesBD = new DataAccess();

            String requete = $"DELETE FROM categorie_materiel WHERE id_categorie = {IdCategorie}";

            accesBD.SetData(requete);
        }

        /// <summary>
        /// Permet de chercher la categorie dans la base de données. 
        /// </summary>
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

        /// <summary>
        /// Permet de modifier les champs de la categorie dans la base données grace à son ID.
        /// </summary>
        public void Update()
        {
            DataAccess accesBD = new DataAccess();

            String requete = $"UPDATE categorie_materiel SET id_categorie = {IdCategorie}, nom_categorie = '{NomCategorie}' WHERE id_categorie = {IdCategorie}";

            accesBD.SetData(requete);
        }



        /// <summary>
        /// Permet de trouver tous les categories qui ont était crée dans la base de données.
        /// </summary>
        /// <returns>La methode FindAll renvoie une liste contentant tout les categories créees.</returns>
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



        /// <summary>
        /// Permet de trouver les categories avec des criteres specifiques.
        /// </summary>
        /// <param name="criteres">Un des champs representant la categorie</param>
        /// <returns>Renvoie une liste de tous les categories qui possedent le critere introduit comme parametre</returns>
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
                   this.NomCategorie == categorie.NomCategorie;
        }

        public static bool operator ==(Categorie? left, Categorie? right)
        {
            return EqualityComparer<Categorie>.Default.Equals(left, right);
        }

        public static bool operator !=(Categorie? left, Categorie? right)
        {
            return !(left == right);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
