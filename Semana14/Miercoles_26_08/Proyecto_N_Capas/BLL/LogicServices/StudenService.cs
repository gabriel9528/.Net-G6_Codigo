using BOL.DataBaseEntities;
using DAL.DataServices;

namespace BLL.LogicServices
{
    public class StudenService : IStudenService
    {
        private readonly IStudentDAL _studentDAL;
        public StudenService(IStudentDAL studentDAL)
        {
            _studentDAL = studentDAL;
        }
        public List<Student> GetListStudentService()
        {
            List<Student> students = new List<Student>();
            students = _studentDAL.GetListStudentDAL();
            return students;
        }
        public bool SaveStudentService(Student student)
        {
            bool result = false;
            if (string.IsNullOrEmpty(student.FirstName) || string.IsNullOrEmpty(student.LastName) || string.IsNullOrEmpty(student.Email))
            {
                result = false;
            }
            else
            {
                result = _studentDAL.SaveStudentDAL(student);
                if (result)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return result;
        }


        public bool UpdateStudentService(Student student)
        {
            bool result = false;
            if (student.Id <= 0 || string.IsNullOrEmpty(student.FirstName) || string.IsNullOrEmpty(student.LastName) || string.IsNullOrEmpty(student.Email))
            {
                return false;
            }
            else
            {
                result = _studentDAL.UpdateStudentDAL(student);
                if (result)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool DeleteStudentService(int id)
        {
            if (id <= 0)
            {
                return false;
            }
            else
            {
                bool result = _studentDAL.DeleteStudentDAL(id);
                if (result)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }



    }
}
