using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Software2_API.Dtos;
using Software2_API.Repositories.Models;
using Software2_API.Utilities;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace Software2_API.Repositories
{
    public class UserRepository
    {
        private readonly TestContext _testContext;

        public UserRepository(TestContext testContext)
        {
            _testContext = testContext;
        }

        public async Task<int> CrearUsuario(RequestUserDto requestUserDto)
        {
            //Consulta SQL
            //bool result = false;
            //string SQL = "INSERT INTO TEST.dbo.[USER] (id_role,id_state,name,username,password) VALUES (" + requestUserDto.IdRole + "," + requestUserDto.IdState + ",'" + requestUserDto.Name + "','" + requestUserDto.Username + "','" + requestUserDto.Password + "');";
            //DBContextUtility Connection = new DBContextUtility();
            //Connection.Connect();
            //using (SqlCommand command = new SqlCommand(SQL, Connection.CONN()))
            //{
            //    int comando = command.ExecuteNonQuery();
            //    if (comando != 0)
            //    {
            //        result = true;
            //    }
            //    else
            //    {
            //        result = false;
            //    }
            //}
            //Connection.Disconnect();

            //return result;

            User user = new();
            user.IdRole = 2;
            user.IdState = 1;
            user.Name = requestUserDto.Name;
            user.Username = requestUserDto.Username;
            user.Password = requestUserDto.Password;

            await _testContext.Users.AddAsync(user);
            return await _testContext.SaveChangesAsync();
        }

        public async Task<ResponseUserDto> Login(InicioSesionDto requestInicioSesionDto)
        {
            //Consulta SQL
            //bool result = false;
            //string SQL = "SELECT * FROM TEST.dbo.[USER] WHERE username = '" + requestInicioSesionDto.NombreUsuario + "' AND password = '" + requestInicioSesionDto.Contrasena + "';";
            //DBContextUtility Connection = new DBContextUtility();
            //Connection.Connect();
            //using (SqlCommand command = new SqlCommand(SQL, Connection.CONN()))
            //{
            //    using (SqlDataReader reader = command.ExecuteReader())
            //    {
            //        if (reader.Read())
            //        {
            //            result = true;
            //        }
            //        else
            //        {
            //            result = false;
            //        }
            //    }
            //}
            //Connection.Disconnect();

            //return result;

            ResponseUserDto responseUser = new();
            User user = await _testContext.Users.FirstOrDefaultAsync(u => u.Username.Equals(requestInicioSesionDto.NombreUsuario) && u.Password.Equals(requestInicioSesionDto.Contrasena));

            if(user != null)
            {
                responseUser.Id_User = user.IdUser;
                responseUser.Id_Role = user.IdRole;
                responseUser.Id_State = user.IdState;
                responseUser.Username = user.Username;
                responseUser.Name = user.Name;
            }

            return responseUser;
        }

        public async Task<List<ResponseUserDto>> ObtenerUsuarios()
        {
            List<ResponseUserDto> listResponseUserDto = new List<ResponseUserDto>();
            //Consulta SQL
            string SQL = "SELECT U.id_user,U.id_role,R.name AS role,U.id_state,S.name AS state,U.name,U.username,U.password " +
                "FROM TEST.dbo.[USER] U " +
                "JOIN TEST.dbo.[ROLE] R ON R.id_rol = U.id_role " +
                "INNER JOIN TEST.dbo.[STATE] S ON S.id_state = U.id_state;";
            DBContextUtility Connection = new DBContextUtility();
            Connection.Connect();
            using (SqlCommand command = new SqlCommand(SQL, Connection.CONN()))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listResponseUserDto.Add(new ResponseUserDto
                        {
                            Id_User = reader.GetInt32(0),
                            Id_Role = reader.GetInt32(1),
                            Role = reader.GetString(2),
                            Id_State = reader.GetInt32(3),
                            State = reader.GetString(4),
                            Name = reader.GetString(5),
                            Username = reader.GetString(6),
                            Password = reader.GetString(7)
                        });
                    }
                }
            }
            Connection.Disconnect();

            return listResponseUserDto;

            //var query = from U in _testContext.Users
            //            join R in _testContext.Roles on U.IdRole equals R.IdRol
            //            join S in _testContext.States on U.IdState equals S.IdState
            //            select new ResponseUserDto
            //            {
            //                Id_User = U.IdUser,
            //                Id_Role = U.IdRole,
            //                Role = R.Name,
            //                Id_State = U.IdState,
            //                State = S.Name,
            //                Name = U.Name,
            //                Username = U.Username,
            //                Password = U.Password
            //            };

            //return await query.ToListAsync();
        }
    }
}
