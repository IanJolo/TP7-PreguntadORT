using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP7_PreguntadORT.Models;

namespace TP7_PreguntadORT.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View("Index");
    }

    public IActionResult ConfigurarJuego(){
        Juegos.InicializarJuego();
        ViewBag.Categorias=BD.ObtenerCategorias();
        ViewBag.Dificultades=BD.ObtenerDificultades();
        return View("ConfigurarJuego");
    }

    public IActionResult Comenzar( int dificultad, int categoria){
        Dictionary<int, List<Preguntas>> preguntas=Juegos.CargarPartida(dificultad,categoria);
        if(preguntas==null){
        return Redirect("ConfigurarJuego");
        }else {
            return Redirect(Url.Action("Jugar", "Home", new {categoria=categoria}));
        }
    }
    public IActionResult Creditos(){
        return View("Creditos");
    }
    public IActionResult RankingGlobal(){
        ViewBag.Usuarios=BD.ObtenerUsuarios();
        return View("RankingGlobal");
    }
    public IActionResult InicioSesion(){
        return View("Ingreso");
    }
    public IActionResult ValidarInicio(string nombreUsuario, string foto){
        BD.InicioSesion(nombreUsuario, foto);
        return Redirect(Url.Action("LandingPage", "Home", new{nombreUsuario}));
    }
    public IActionResult LandingPage(string nombreUsuario){
        ViewBag.DatosUsuario=BD.ObtenerInfoUsuario(nombreUsuario);
        Juegos.CargarUsername(nombreUsuario);
        
        return View("LandingPage");
    }
     public IActionResult Jugar(int categoria){
        ViewBag.NombreUsuario=Juegos.username;
        ViewBag.Puntaje=Juegos.puntajeActual;
        if(ViewBag.Puntaje==null){
            ViewBag.Puntaje=0;
        }
        return View("Juego"); 
    }

    public IActionResult MomentoPreg(string nombrecategoria){
        int idCategoRuleta=1;
        if(nombrecategoria=="HISTORIA"){
            idCategoRuleta=2;
        }else if(nombrecategoria=="CIENCIA"){
            idCategoRuleta=3;
        }else if(nombrecategoria=="DEPORTE"){
            idCategoRuleta=4;
        }else if(nombrecategoria=="GEOGRAFIA"){
            idCategoRuleta=1;
        }else {
            Console.WriteLine("ERROR");
        }
       ViewBag.Pregunta = Juegos.ObtenerProximaPregunta(idCategoRuleta);
    ViewBag.CategoRuleta = idCategoRuleta;

if (ViewBag.Pregunta == null)
{
    ViewBag.NombreUsuario=Juegos.username;
    ViewBag.Puntaje=Juegos.puntajeActual;
    return View("Fin");
}else {
            ViewBag.NombreUsuario=Juegos.username;
            ViewBag.Puntaje=Juegos.puntajeActual;
            ViewBag.Contador=Juegos.contador+1;
            ViewBag.Respuestas=Juegos.ObtenerProximasRespuestas(ViewBag.Pregunta.IdPregunta);      
        return View("MomentoPreg");
        }
    }
[HttpPost]
public IActionResult VerificarRespuesta(int idPregunta, int idRespuesta, int idCategoria){
    int puntaje=0;
    string nombre="";
    Respuestas respuestitia=new Respuestas();
    List<Respuestas> listaRespuestas=new List<Respuestas>();
    listaRespuestas=Juegos.ObtenerRespuestas();
    ViewBag.respuestas = listaRespuestas;
    foreach(Respuestas respuesta in listaRespuestas){
        if(respuesta.Correcta==true){
            respuestitia=respuesta;
        }
    }
    ViewBag.FueCorrecta=Juegos.VerificarRespuesta(idPregunta,idRespuesta, idCategoria);
    if(ViewBag.FueCorrecta==false){
        puntaje=Juegos.puntajeActual;
        nombre=Juegos.username;
    }
BD.ActualizarPuntaje(puntaje, nombre);
     ViewBag.Contador=Juegos.contador;
    ViewBag.Puntaje=Juegos.puntajeActual;
    ViewBag.RespuestaCorrecta=respuestitia;
    return View("Respuesta");
}

public IActionResult Juego(){
    return View("Juego");
}

public IActionResult Fin(){
    ViewBag.NombreUsuario=Juegos.username;
    ViewBag.Puntaje=Juegos.puntajeActual;
    return View("Fin");
}
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
