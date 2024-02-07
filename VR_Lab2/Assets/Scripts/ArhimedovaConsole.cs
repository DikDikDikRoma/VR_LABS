using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArhimedovaConsole : MonoBehaviour
{
    public GameObject[] buttons;//список кнопок
    public GameObject particle;//эффект трубы
    public GameObject Water;//жидксть
    public Color[] w1;//цвета жидкости
    bool WaterFull;//для анимации подъёма жидкости
    public GameObject menu;// меню кнопок
    public GameObject cube;// куб для проверки закона Архимеда
    public Transform CubePos;//место появления кубика
    public GameObject sphere;// куб для проверки закона Архимеда
    public Transform spherePos;//место появления кубика
    public void ButtonPush(GameObject button)
    {

        for (int i = 0; i < buttons.Length; i++)
        {
            if (button == buttons[i])//если есть соответствие по кнопке
            {
                if (i<3 && !WaterFull) //если кнопка из диапазона выбора жидкости и жидкости сейчас нет, задаём цвет жидкости и частиц, затем включаем напонение бака
                {
                    Water.GetComponent<Renderer>().material.color = w1[i];
                    particle.GetComponent<ParticleSystem>().startColor = w1[i];                  
                    particle.SetActive(true);
                    particle.GetComponent<ParticleSystem>().playOnAwake  =true;
                    Water.SetActive(true);
                    WaterFull = true;
                }else if (i == 3)//если наажта кнопка сброса жидкости
                {
                    WaterFull = false;
                }else if (i==4)//если нажата кнопка создания кубика
                {
                    Instantiate(cube, CubePos);
                }else if (i==5)//если нажата кнопка включения/выключения меню
                {
                    menu.SetActive(!menu.activeSelf);
                }else if (i==6)
                {
                    Instantiate(cube, CubePos);
                }
                else if (i==7)
                {
                    Instantiate(sphere, spherePos);
                }
            }       
        }
    }

    void Update()
    {
        if (WaterFull && Water.transform.localScale.y < 16)
        {
            Water.transform.localScale += new Vector3(0, 1, 0) * Time.deltaTime;
        }
        if (!WaterFull && Water.transform.localScale.y > 9.6f)
        {
            particle.SetActive(false);
            Water.transform.localScale -= new Vector3(0, 1, 0) * Time.deltaTime;
        }
    }
}
