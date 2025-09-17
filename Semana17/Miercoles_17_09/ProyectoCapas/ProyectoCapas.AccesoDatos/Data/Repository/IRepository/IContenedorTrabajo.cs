namespace ProyectoCapas.AccesoDatos.Data.Repository.IRepository
{
    public interface IContenedorTrabajo : IDisposable
    {
        ICategoriaRepository ICategoriaRepository { get; }
        void Save();
    }
}
