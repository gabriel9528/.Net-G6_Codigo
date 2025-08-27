using BOL.DataBaseEntities;

namespace DAL.DataServices
{
    public interface IStudentDAL
    {
        List<Student> GetListStudentDAL();
        bool SaveStudentDAL(Student student);
        bool UpdateStudentDAL(Student student);
        bool DeleteStudentDAL(int id);
    }
}
