using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_MATINFO.Model
{
    /// <summary>
    /// Stocke 7 informations :
    /// 2 entier : l'ID du materiel et la FKidCategorie.
    /// 3 string : le nom, le code barre du materie et la reference du constructeur.
    /// 1 categorie : la categorie du materiel.
    /// 1 observable collection : liste des attributions du materiel.
    /// </summary> 
    public class Materiel : Crud<Materiel>, ICloneable
    {
        private int idMateriel;
        private int fKIdCategorie;

        private string nomMateriel;
        private string codeBarre;
        private string referenceConstructeur;

        private Categorie categorie;
        private ObservableCollection<Attribution> attributions = new ObservableCollection<Attribution>();


        /// <summary>
        /// L'ID est unique et definit le materiel
        /// </summary>
        public int IdMateriel
        {
            get
            {
                return this.idMateriel;
            }

            set
            {
                this.idMateriel = value;
            }
        }

        /// <summary>
        /// La FKIdCategorie definit la clé etrangere relié à l'Id categorie
        /// </summary>
        public int FKIdCategorie
        {
            get
            {
                return this.fKIdCategorie;
            }

            set
            {
                this.fKIdCategorie = value;
            }
        }


        /// <summary>
        /// Définit le nom du materiel. 
        /// </summary>
        /// <exception cref="ArgumentException"> Envoyée si le nom du materiel n'est pas saisie. 
        public string NomMateriel
        {
            get
            {
                return this.nomMateriel;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Le champs NomMateriel doit etre sasie");

                this.nomMateriel = value;
            }
        }


        /// <summary>
        /// Le code barre est unique à chaque materiel. 
        /// </summary>
        /// <exception cref="ArgumentException"> Envoyée si le code barre du materiel n'est pas saisie.
        public string CodeBarre
        {
            get
            {
                return this.codeBarre;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Le champs CodeBarre doit etre sasie");

                this.codeBarre = value.ToUpper();
            }
        }


        /// <summary>
        /// Définit la reference du constructeur du materiel.
        /// </summary>
        /// <exception cref="ArgumentException"> Envoyée si la reference du constructeur du materiel n'est pas saisie.
        public string ReferenceConstructeur
        {
            get
            {
                return this.referenceConstructeur;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Le champs Reference constructeur doit etre sasie");

                this.referenceConstructeur = value.ToUpper();
            }
        }


        /// <summary>
        /// Définit la categorie du materiel
        /// </summary>
        public Categorie Categorie
        {
            get
            {
                return this.categorie;
            }

            set
            {
                this.FKIdCategorie = value.IdCategorie;
                this.categorie = value;
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

        public Materiel() { }

        public Materiel(int idMateriel)
        {
            IdMateriel = idMateriel;
        }

        /// <summary>
        /// Constructeur de la classe Materiel.
        /// </summary>
        /// <param name="idMateriel">L'ID du materiel.</param>
        /// <param name="fkIdCategorie">La fkIdcategorie du materiel.</param>
        /// <param name="nomMateriel">Le nom du materiel.</param>
        /// <param name="codeBarre">Le code barre du materiel.</param>
        /// <param name="referenceConstructeur">La reference constructeur du materiel.</param>
        /// <remarks>Ce constructeur initialise les propriétés idMateriel, fkIdCategorie, nomMateriel, codeBarre et referenceConstructeur de l'objet Materiel.</remarks>
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


        /// <summary>
        /// Permet la creation d'un Materiel dans la base de données.
        /// </summary>
        public void Create()
        {
            DataAccess accesBD = new DataAccess();

            String requete = $"INSERT INTO materiel (nom_materiel, id_categorie, code_barre, reference_constructeur) VALUES ('{NomMateriel}', {FKIdCategorie}, '{CodeBarre}', '{ReferenceConstructeur}')";

            accesBD.SetData(requete);
            this.Read();
        }

        /// <summary>
        /// Permet la suppression d'un Materiel de la base de données.
        /// </summary>
        public void Delete()
        {
            DataAccess accesBD = new DataAccess();

            String requete = $"DELETE FROM materiel WHERE id_materiel = {IdMateriel}";

            accesBD.SetData(requete);
        }


        /// <summary>
        /// Permet de chercher le Materiel dans la base de données. 
        /// </summary>
        public void Read()
        {
            DataAccess accesBD = new DataAccess();

            String requete = $"SELECT * FROM materiel WHERE nom_materiel = '{NomMateriel}'";

            DataTable data = accesBD.GetData(requete);

            if (data != null && data.Rows.Count > 0)
            {
                IdMateriel = (int)data.Rows[0]["id_materiel"];
                FKIdCategorie = (int)data.Rows[0]["id_categorie"];

                NomMateriel = (string)data.Rows[0]["nom_materiel"];
                CodeBarre = (string)data.Rows[0]["code_barre"];
                ReferenceConstructeur = (string)data.Rows[0]["reference_constructeur"];
            }
        }

        /// <summary>
        /// Permet de modifier les champs du Materiel dans la base données grace à son ID.
        /// </summary>
        public void Update()
        {
            DataAccess accesBD = new DataAccess();

            String requete = $"UPDATE materiel SET nom_materiel = '{NomMateriel}', id_categorie = {FKIdCategorie}, code_barre = '{CodeBarre}', reference_constructeur = '{ReferenceConstructeur}' WHERE id_materiel = {IdMateriel}";

            accesBD.SetData(requete);
        }

        /// <summary>
        /// Permet de trouver tous les Materiels qui ont était crée dans la base de données.
        /// </summary>
        /// <returns>La methode FindAll renvoie une liste contentant tout les Materiels crées.</returns>
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


        /// <summary>
        /// Permet de trouver les materiels avec des criteres specifiques.
        /// </summary>
        /// <param name="criteres">Un des champs representant le Materiel</param>
        /// <returns>Renvoie une liste de tous les materiels qui possedent le critere introduit comme parametre</returns>
        public ObservableCollection<Materiel> FindBySelection(string criteres)
        {
            ObservableCollection<Materiel> materiels = new ObservableCollection<Materiel>();
            DataAccess accesBD = new DataAccess();

            String requete = $"SELECT * FROM materiel WHERE {criteres}";

            DataTable data = accesBD.GetData(requete);

            if (data != null)
            {
                Console.WriteLine(data.Rows.Count);

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



        /// <summary>
        /// Indique si l'objet actuel est égal à un autre objet du même type.
        /// </summary>
        /// <param name="obj">Objet à comparer à cet objet</param>
        /// <returns>
        ///   <c>true</c> si l'objet spécifié est égal à l'objet actuel ; sinon, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj)
        {
            return obj is Materiel materiel &&
                   this.IdMateriel == materiel.IdMateriel &&
                   this.FKIdCategorie == materiel.FKIdCategorie &&
                   this.NomMateriel == materiel.NomMateriel &&
                   this.CodeBarre == materiel.CodeBarre &&
                   this.ReferenceConstructeur == materiel.ReferenceConstructeur;
        }

        /// <summary>
        /// Indique si deux objets Materiel sont égaux.
        /// </summary>
        /// <param name="left">Le premier Materiel à comparer.</param>
        /// <param name="right">Le deuxième Materieel à comparer.</param>
        /// <returns>
        ///   <c>true</c> si les objets sont égaux ; sinon, <c>false</c>.
        public static bool operator ==(Materiel? left, Materiel? right)
        {
            return EqualityComparer<Materiel>.Default.Equals(left, right);
        }

        /// <summary>
        /// Indique si deux Materiel ne sont pas égaux.
        /// </summary>
        /// <param name="left">Le premier Materiel à comparer.</param>
        /// <param name="right">Le deuxième Materiel à comparer.</param>
        /// <returns>
        ///   <c>true</c> si les objets ne sont pas égaux ; sinon, <c>false</c>.
        /// </returns>   
        public static bool operator !=(Materiel? left, Materiel? right)
        {
            return !(left == right);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }


        /// <summary>
        /// Retourne le code de hachage pour cette structure Materiel.
        /// </summary>
        /// <returns>Entier qui représente le code de hachage pour ce Materiel.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(this.IdMateriel, this.FKIdCategorie, this.NomMateriel, this.CodeBarre, this.ReferenceConstructeur);
        }
    }
}
