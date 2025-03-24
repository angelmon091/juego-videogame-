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
    public Playercontroller vidajugador;
    public int indexActual;
    public Sprite corazonLleno;
    public Sprite corazonMitad; // Añadido para el corazón a la mitad
    public Sprite corazonVacio;

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
        for (int i = 0; i < cantidadmaximavida / 2; i++)
        {
            GameObject corazon = Instantiate(corazonesPrefab, transform);
            listaCorazones.Add(corazon.GetComponent<Image>());
        }
        indexActual = listaCorazones.Count - 1;
    }

    private void CambiarVida(int vidaActual)
    {
        int vidaRestante = vidaActual;
        for (int i = 0; i < listaCorazones.Count; i++)
        {
            if (vidaRestante >= 2)
            {
                listaCorazones[i].sprite = corazonLleno;
                vidaRestante -= 2;
            }
            else if (vidaRestante == 1)
            {
                listaCorazones[i].sprite = corazonMitad;
                vidaRestante = 0;
            }
            else
            {
                listaCorazones[i].sprite = corazonVacio;
            }
        }
    }
}