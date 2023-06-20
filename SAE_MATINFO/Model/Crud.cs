using System.Collections.ObjectModel;

namespace SAE_MATINFO.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface Crud<T>
   {
      void Create();
      void Read();   
      void Update();
      void Delete();
      
      ObservableCollection<T> FindAll();     
      ObservableCollection<T> FindBySelection(string criteres);
   }
}