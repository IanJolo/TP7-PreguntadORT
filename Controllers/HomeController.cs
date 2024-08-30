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

    public IActionResult Comenzar(string username, int dificultad, int categoria){
        Juegos.CargarPartida(username,dificultad,categoria);
        return View();
    }
    public IActionResult Creditos(){
        return View("Creditos");
    }
    public IActionResult RankingGlobal(){
        return View("RankingGlobal");
    }
    public IActionResult InicioSesion(){
        return View("InicioSesion");
    }
     public IActionResult Jugar(){
        ViewBag.Pregunta=Juegos.ObtenerProximaPregunta();
         if(ViewBag.Pregunta!=null){
            ViewBag.Respuestas=Juegos.ObtenerProximasRespuestas(ViewBag.Pregunta.idPregunta);      
            return View("Juego"); 
        }else {
            return View("Fin");
        }
       
    }
[HttpPost]
public IActionResult VerificarRespuesta(int idPregunta, int idRespuesta){
    foreach(Respuestas respuesta in ViewBag.Respuestas){
        if(respuesta.Correcta==true){
            ViewBag.RespuestaCorrecta=respuesta;
        }
    }
    ViewBag.FueCorrecta=Juegos.VerificarRespuesta(idPregunta,idRespuesta);
    return View("Respuesta");
}









    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
