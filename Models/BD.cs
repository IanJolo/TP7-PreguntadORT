namespace TP7_PreguntadORT.Models;
using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
public static class BD{
   private static string _connectionString = @"Server = A-PHZ2-CIDI-13;DataBase = PreguntadOrt;Trusted_Connection=True;";

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
    public static List<Preguntas> ObtenerPreguntas(int dificultad, int categoria){
            string sql;
            List<Preguntas> _ListadoPreguntas=new List<Preguntas>();
            using(SqlConnection db = new SqlConnection(_connectionString)){
            if(dificultad==-1 && categoria==-1){
                 sql= "SELECT * from Preguntas order by NEWID()";
            _ListadoPreguntas=db.Query<Preguntas>(sql).ToList();
            }else if(categoria==-1){
                 sql= "SELECT * from Preguntas where IdDificultad=@dififi order by NEWID()";
            _ListadoPreguntas=db.Query<Preguntas>(sql, new {dififi=dificultad}).ToList();
            }else if(dificultad==-1){
             sql= "SELECT * from Preguntas where IdCategoria=@cateogr order by NEWID()";
            _ListadoPreguntas=db.Query<Preguntas>(sql, new {cateogr=categoria}).ToList();
            }else {
                 sql= "SELECT * from Preguntas where IdCategoria=@cateogr AND IdDificultad=@dififi order by NEWID() ";
            _ListadoPreguntas=db.Query<Preguntas>(sql, new {cateogr=categoria, dififi=dificultad}).ToList();
            }
        }
        return _ListadoPreguntas; 
    }
    public static List<Respuestas>? ObtenerRespuesta(int IdPregunta){
        List<Respuestas> ListaRespuesta;
        using(SqlConnection db= new SqlConnection(_connectionString)){
            string sql="SELECT * from Respuestas where IdPregunta=@preguntiti";
            ListaRespuesta=db.Query<Respuestas>(sql, new {preguntiti = IdPregunta}).ToList();
        }
        return ListaRespuesta;
    }
}
