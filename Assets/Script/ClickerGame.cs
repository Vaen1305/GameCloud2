using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickerGame : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Referencia al texto del puntaje
    public TextMeshProUGUI multiplierText; // Referencia al texto del multiplicador (opcional)
    public int score = 0; // Puntaje inicial
    public int multiplier = 1; // Multiplicador inicial

    void Start()
    {
        UpdateUI(); // Actualizamos la interfaz al iniciar
    }

    // Este método se llama cuando el botón es presionado
    public void OnClick()
    {
        score += multiplier; // Incrementamos el puntaje con el multiplicador
        UpdateUI(); // Actualizamos el texto
    }

    // Método para incrementar el multiplicador
    public void UpgradeMultiplier()
    {
        if (score >= 10) // Requisito: 10 puntos para subir el multiplicador
        {
            score -= 10; // Reducimos puntos
            multiplier++; // Aumentamos el multiplicador
            UpdateUI(); // Actualizamos la interfaz
        }
    }

    // Método para actualizar la interfaz
    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        if (multiplierText != null)
        {
            multiplierText.text = "Multiplier: x" + multiplier;
        }
    }
}
