using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;


public class ControllerGrabObject : MonoBehaviour
{
    public SteamVR_Input_Sources handType;//выбранная рука
    public SteamVR_Behaviour_Pose controllerPose;//используемый контроллер
    public SteamVR_Action_Boolean grabAction;//кнопка захвата
    public int room;//в какой мы комнате
    private GameObject collidingObject; //объект, которого косается геймпад
    private GameObject objectInHand; //взяты объект
    ArhimedovaConsole ArhimedovaConsole;
    ImpulseConsole ImpulseConsole;

    private void SetCollidingObject(Collider col)
    {
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
        Debug.Log("Grab " + col.tag);
        collidingObject = col.gameObject;
    }//установка взятого объекта

    private void Update()
    {
        if (grabAction.GetLastStateDown(handType))//если нажат захват
        {
            if (collidingObject)//если есть захваченный объект 
            {
                if (collidingObject.tag == "Button")
                {
                    Debug.Log("Button is work");
                    ArhimedovaConsole = GameObject.FindGameObjectWithTag("Finish").GetComponent<ArhimedovaConsole>();
                    if (ArhimedovaConsole != null)
                    {
                        ArhimedovaConsole.ButtonPush(collidingObject.gameObject);
                    }

                    ImpulseConsole = GameObject.FindGameObjectWithTag("Finish").GetComponent<ImpulseConsole>();
                    if (ImpulseConsole != null)
                    {
                        ImpulseConsole.ButtonPush(collidingObject.gameObject);
                    }

                }
            }
        }

        if (grabAction.GetLastStateUp(handType))//если захват отпущен
        {
            if (objectInHand)//если есть захваченный объект 
            {
                ReleaseObject();// отпустить объект 
            }
        }
    }

    private void ReleaseObject()//подбор объекта
    {
        if (GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            objectInHand.GetComponent<Rigidbody>().velocity = controllerPose.GetVelocity();
            objectInHand.GetComponent<Rigidbody>().angularVelocity = controllerPose.GetAngularVelocity();

        }//присоединение объекта к контроллеру
        objectInHand = null;
    }

    public void OnTriggerEnter(Collider other)
    {

        SetCollidingObject(other);
        if (other.name == "Laser")//Если взяли указку, сообщаем ей это
        {
            //other.GetComponent<LaserPoiner>().InHand = true;
        }
    }//проверка касание контроллера с любым объектом

    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }//проверка удержания контроллера любого объекта

    public void OnTriggerExit(Collider other)
    {
        if (other.name == "Laser")//Если убрали указку, сообщаем ей это
        {
            //other.GetComponent<LaserPoiner>().InHand = false;
        }
        if (!collidingObject)
        {
            return;
        }
        collidingObject = null;
    }//проверка отпускание контроллеро объекта
}
