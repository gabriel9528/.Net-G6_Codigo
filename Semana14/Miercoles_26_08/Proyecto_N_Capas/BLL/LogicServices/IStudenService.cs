using BOL.DataBaseEntities;

namespace BLL.LogicServices
{
    public interface IStudenService
    {
        List<Student> GetListStudentService();
        bool SaveStudentService(Student student);
        bool UpdateStudentService(Student student);
        bool DeleteStudentService(int id);
    }
}
