namespace TP7_PreguntadORT.Models;
using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
public static class BD{
   private static string _connectionString = @"Server = A-PHZ2-AMI-08;DataBase = PreguntadOrt;Trusted_Connection=True;";

   public static List<Categoria> ObtenerCategorias(){
        List<Categoria> _ListadoCategoria=new List<Categoria>();
        using(SqlConnection db = new SqlConnection(_connectionString)){
            string sql= "SELECT * from Categorias";
            _ListadoCategoria=db.Query<Categoria>(sql).ToList();
        }
        return _ListadoCategoria;
   }
     public static List<Dificultades> ObtenerDificultades(){
        List<Dificultades> _ListadoDificultad=new List<Dificultades>();
        using(SqlConnection db = new SqlConnection(_connectionString)){
            string sql= "SELECT * from Dificultades";
            _ListadoDificultad=db.Query<Dificultades>(sql).ToList();
        }
        return _ListadoDificultad;
    }
    public static Dictionary<int, List<Preguntas>> ObtenerPreguntas(int dificultad, int categoria){
            string sql;
            Dictionary<int, List<Preguntas>> DicCategorias = new Dictionary<int, List<Preguntas>>();
            List<Preguntas> _ListadoPreguntas=new List<Preguntas>();
            List<Preguntas> _ListadoPreguntas2 = new List<Preguntas>(); 
            List<Preguntas> _ListadoPreguntas3 = new List<Preguntas>(); 
            List<Preguntas> _ListadoPreguntas4 = new List<Preguntas>(); 
            using(SqlConnection db = new SqlConnection(_connectionString)){
            if(dificultad==-1 && categoria==-1){
                sql= "SELECT * from Preguntas where idCategoria=@cateogr order by NEWID()";
            _ListadoPreguntas=db.Query<Preguntas>(sql, new {cateogr=1}).ToList();
            sql= "SELECT * from Preguntas where idCategoria=@cateogr order by NEWID()";
            _ListadoPreguntas2=db.Query<Preguntas>(sql, new {cateogr=2}).ToList();
            sql= "SELECT * from Preguntas  where idCategoria=@cateogr order by NEWID()";
            _ListadoPreguntas3=db.Query<Preguntas>(sql, new {cateogr=3}).ToList();
            sql= "SELECT * from Preguntas where idCategoria=@cateogr order by NEWID()";
            _ListadoPreguntas4=db.Query<Preguntas>(sql, new {cateogr=4}).ToList();
            DicCategorias.Add(1,_ListadoPreguntas);
            DicCategorias.Add(2,_ListadoPreguntas2);
            DicCategorias.Add(3,_ListadoPreguntas3);
            DicCategorias.Add(4,_ListadoPreguntas4);
            }else if(categoria==-1){
                 sql= "SELECT * from Preguntas where IdCategoria=@cateogr AND IdDificultad=@dififi order by NEWID() ";
            _ListadoPreguntas=db.Query<Preguntas>(sql, new {cateogr=1, dififi=dificultad}).ToList();
            sql= "SELECT * from Preguntas where IdCategoria=@cateogr AND IdDificultad=@dififi order by NEWID() ";
            _ListadoPreguntas2=db.Query<Preguntas>(sql, new {cateogr=2, dififi=dificultad}).ToList();
            sql= "SELECT * from Preguntas where IdCategoria=@cateogr AND IdDificultad=@dififi order by NEWID() ";
            _ListadoPreguntas3=db.Query<Preguntas>(sql, new {cateogr=3, dififi=dificultad}).ToList();
            sql= "SELECT * from Preguntas where IdCategoria=@cateogr AND IdDificultad=@dififi order by NEWID()";
            _ListadoPreguntas4=db.Query<Preguntas>(sql, new {cateogr=4, dififi=dificultad}).ToList();
            DicCategorias.Add(1,_ListadoPreguntas);
            DicCategorias.Add(2,_ListadoPreguntas2);
            DicCategorias.Add(3,_ListadoPreguntas3);
            DicCategorias.Add(4,_ListadoPreguntas4);
            }else if(dificultad==-1){
             sql= "SELECT * from Preguntas where IdCategoria=@cateogr order by NEWID()";
            _ListadoPreguntas=db.Query<Preguntas>(sql, new {cateogr=categoria}).ToList();
            DicCategorias.Add(categoria,_ListadoPreguntas);
            }else {
                 sql= "SELECT * from Preguntas where IdCategoria=@cateogr AND IdDificultad=@dififi order by NEWID() ";
            _ListadoPreguntas=db.Query<Preguntas>(sql, new {cateogr=categoria, dififi=dificultad}).ToList();
            DicCategorias.Add(categoria,_ListadoPreguntas);
            }
        }
        return DicCategorias; 
    }
    public static List<Respuestas>? ObtenerRespuesta(int IdPregunta){
        List<Respuestas> ListaRespuesta;
        using(SqlConnection db= new SqlConnection(_connectionString)){
            string sql="SELECT * from Respuestas where IdPregunta=@preguntiti";
            ListaRespuesta=db.Query<Respuestas>(sql, new {preguntiti = IdPregunta}).ToList();
        }
        return ListaRespuesta;
    }

    public static void InicioSesion(string usuario, string foto){
       
        int usuario_id;
        using(SqlConnection db= new SqlConnection(_connectionString)){
            string sql="SELECT idUsuario from Usuarios where nombre=@pUsuario";
            usuario_id = db.QueryFirstOrDefault<int>(sql, new {pUsuario = usuario});
        }
            if (usuario_id==0){
                using(SqlConnection db1= new SqlConnection(_connectionString)){
            string sql1="INSERT into Usuarios (nombre, foto) VALUES (@usuarito, @fotito)";
            int cant= db1.Execute(sql1, new {usuarito = usuario, fotito=foto});
            
            }
        }
    
}

public static Usuario ObtenerInfoUsuario(string nombreUsuario){
    Usuario infoUSuario=new Usuario();
    using(SqlConnection db = new SqlConnection(_connectionString)){
            string sql= "SELECT * from Usuarios where nombre=@nombresito";
            infoUSuario=db.QueryFirstOrDefault<Usuario>(sql, new{nombresito=nombreUsuario});
        }
    return infoUSuario;
}

public static List<Usuario> ObtenerUsuarios(){
     List<Usuario> _ListaUsuarios=new List<Usuario>();
        using(SqlConnection db = new SqlConnection(_connectionString)){
            string sql= "SELECT * from Usuarios";
            _ListaUsuarios=db.Query<Usuario>(sql).ToList();
        }
        return _ListaUsuarios;
}
public static void ActualizarPuntaje(int puntaje, string nombre){
    using(SqlConnection db = new SqlConnection(_connectionString)){
            string sql= "UPDATE Usuarios set puntaje=@puntajesito where nombre=@nombresito";
            db.Execute(sql, new {puntajesito=puntaje, nombresito=nombre});
        }
}
}
