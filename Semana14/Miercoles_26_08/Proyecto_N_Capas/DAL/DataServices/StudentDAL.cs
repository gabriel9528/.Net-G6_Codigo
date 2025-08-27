using System.Data;
using BOL.DataBaseEntities;
using DAL.DataContext;
using Dapper;

namespace DAL.DataServices
{
    public class StudentDAL : IStudentDAL
    {
        private readonly IDapperConnectionHelper _dapperConnectionHelper;
        public StudentDAL(IDapperConnectionHelper dapperConnectionHelper)
        {
            _dapperConnectionHelper = dapperConnectionHelper;
        }

        public List<Student> GetListStudentDAL()
        {
            List<Student> students = new List<Student>();
            try
            {
                using (IDbConnection dbConnection = _dapperConnectionHelper.GetDapperConnectionHelper())
                {
                    string sqlQuery = "SELECT * FROM Student";
                    students = dbConnection.Query<Student>(sqlQuery).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de estudiantes", ex);
            }
            return students;
        }


        public bool SaveStudentDAL(Student student)
        {
            bool result = false;
            try
            {
                using(IDbConnection dbConnection = _dapperConnectionHelper.GetDapperConnectionHelper())
                {
                    string query = "INSERT INTO Student (FirstName, LastName, Email, IsActive) " +
                        "VALUES (@FirstName, @LastName, @Email, @IsActive)";
                    dbConnection.Execute(query, new
                    {
                        FirstName = student.FirstName,
                        LastName = student.LastName,
                        Email = student.Email,
                        IsActive = 1
                    }, commandType:CommandType.Text);
                }
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }



        public bool UpdateStudentDAL(Student student)
        {
            bool result = false;
            try
            {
                using (IDbConnection dbConnection = _dapperConnectionHelper.GetDapperConnectionHelper())
                {
                    string query = "Update Student set FirstName = @FirstName, LastName = @LastName, Email = @Email where Id = Id";
                    dbConnection.Execute(query, new
                    {
                        Id = student.Id,
                        FirstName = student.FirstName,
                        LastName = student.LastName,
                        Email = student.Email
                    }, commandType: CommandType.Text);
                }
                result = true; ;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }


        public bool DeleteStudentDAL(int id)
        {
            bool result = false;
            try
            {
                using(IDbConnection dbconnection = _dapperConnectionHelper.GetDapperConnectionHelper())
                {
                    string query = "Update Student set IsActive = 0 where Id = @Id";
                    dbconnection.Execute(query, new
                    {
                        Id = id
                    }, commandType: CommandType.Text);
                }
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

    }
}
