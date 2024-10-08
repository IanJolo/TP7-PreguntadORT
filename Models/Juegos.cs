namespace TP7_PreguntadORT.Models;

public class Juegos{
    public  static string? username {get;set;}
    public static int puntajeActual{get;set;}
    private static int cantidadPreguntasCorrectas{get;set;}
    private static Dictionary<int,List<Preguntas>> preguntas{get;set;}
    private static List<Respuestas>? respuestas{get;set;}
    public static int contador{get;set;}

    public static void InicializarJuego()
    {
        contador=0;
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
 public static Dictionary<int,List<Preguntas>> CargarPartida( int dificultad, int categoria){
    Dictionary<int,List<Preguntas>> DicPreguntas=BD.ObtenerPreguntas(dificultad, categoria);
    preguntas=DicPreguntas;  
    return DicPreguntas;
}
 public static Preguntas ObtenerProximaPregunta(int CateRuleta)
{
    Preguntas pregunta = null;

    if (preguntas.ContainsKey(CateRuleta))
    {
        List<Preguntas> lalista = preguntas[CateRuleta];

        if (lalista != null && lalista.Count > 0)
        {
            pregunta = lalista[0];  
            lalista.RemoveAt(0);    
        }
    }

    return pregunta;
}

public static List<Respuestas>? ObtenerProximasRespuestas(int idPregunta){
    List<Respuestas>? respuestasPreguntas=BD.ObtenerRespuesta(idPregunta);
    respuestas=respuestasPreguntas;
    return respuestasPreguntas;
}

public static bool VerificarRespuesta(int idPregunta, int idRespuesta, int idCategoria)
{
    bool esCorrecto = false;

    foreach (Respuestas respuesta in respuestas)
    {
        if (respuesta.IdRespuesta == idRespuesta && respuesta.Correcta)
        {
            esCorrecto = true;
            cantidadPreguntasCorrectas++;
            if (preguntas[idCategoria].Count > 0)
            {
                if (preguntas[idCategoria][0].IdDificultad == 1)
                {
                    puntajeActual =puntajeActual+ 100;
                }
                else if (preguntas[idCategoria][0].IdDificultad == 2)
                {
                    puntajeActual =puntajeActual+ 250;
                }
                else
                {
                    puntajeActual =puntajeActual+ 500;
                }

                contador++;

                var objetoAEliminar = preguntas[idCategoria].FirstOrDefault(o => o.IdPregunta == idPregunta);
                preguntas[idCategoria].Remove(objetoAEliminar);
                
            }
        }
    }

    return esCorrecto;
}
public static List<Respuestas> ObtenerRespuestas(){
    List<Respuestas> listaRespuestas= new List<Respuestas>();
    listaRespuestas=respuestas;
    return listaRespuestas;
}

public static int ObtenerPuntaje(){
    return puntajeActual;
}
public static int ObtenerContador(){
    return contador;
}
public static void CargarUsername(string usernamee){
    username=usernamee;
}
}