namespace TP7_PreguntadORT.Models;

public class Juegos{
    private static string? username {get;set;}
    private static int puntajeActual{get;set;}
    private static int cantidadPreguntasCorrectas{get;set;}
    private static List<Preguntas>? preguntas{get;set;}
    private static List<Respuestas>? respuestas{get;set;}
    public static int contador{get;set;}

    public static void InicializarJuego()
    {
        contador=0;
        username= null; 
        puntajeActual=0;
        cantidadPreguntasCorrectas= 0;
        preguntas= null;
        respuestas=null;
    }
 public static List<Categoria>? ObtenerCategorias(){
    List<Categoria> ListaCategorias=BD.ObtenerCategorias();
    return ListaCategorias;
}
 public static List<Dificultades> ObtenerDificultades(){
    List<Dificultades> ListaDificultades=BD.ObtenerDificultades();
    return ListaDificultades;
}
 public static void CargarPartida(string username, int dificultad, int categoria){
    List<Preguntas> ListaPreguntas=BD.ObtenerPreguntas(dificultad, categoria);
    preguntas=ListaPreguntas;  
}
 public static Preguntas ObtenerProximaPregunta(){
    Preguntas Pregunta=null;
    if(contador<preguntas.Count()){
    Pregunta=preguntas[contador];
    }
    return Pregunta;
}
public static List<Respuestas>? ObtenerProximasRespuestas(int idPregunta){
    List<Respuestas>? respuestasPreguntas=BD.ObtenerRespuesta(idPregunta);
    respuestas=respuestasPreguntas;
    return respuestasPreguntas;
}
public static bool VerificarRespuesta(int idPregunta, int idRespuesta){
    bool esCorrecto=false;
    if(respuestas[idRespuesta].Correcta==true){
        esCorrecto=true;
        cantidadPreguntasCorrectas++;
        puntajeActual=puntajeActual+100;
        contador++;
    }
    return esCorrecto;
}
}