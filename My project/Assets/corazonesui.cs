using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using Unity.VisualScripting;

public class corazonesui : MonoBehaviour
{
    public List<Image> listaCorazones;
    public GameObject corazonesPrefab;
    public Playercontroller vidajugador; // Asegúrate de que esta variable esté asignada en el Inspector
    public int indexActual;
    public Sprite corazonLleno;
    public Sprite corazonVacio;
    public Sprite corazonMitad;

    private void Awake()
    {
        vidajugador.cambioVida.AddListener(CambiarCorazones);
    }

    private void CambiarCorazones(int vidaActual)
    {
        if (!listaCorazones.Any())
        {
            CrearCorazones(vidaActual);
        }
        else
        {
            CambiarVida(vidaActual);
        }
    }

    private void CrearCorazones(int cantidadmaximavida)
    {
        for (int i = 0; i < cantidadmaximavida; i++)
        {
            GameObject corazon = Instantiate(corazonesPrefab, transform);
            listaCorazones.Add(corazon.GetComponent<Image>());
        }
        indexActual = cantidadmaximavida - 1;
    }

    private void CambiarVida(int vidaActual)
    {
        int vidaRestante = vidaActual;
        
        // Implementa aquí la lógica para cambiar la vida
    }
}