using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject player;
    private Vector3 offset;
    [SerializeField] Vector3 offsetValues;

    void Start()
    {
        //Aqui a gente atribui os valores de modificação da posição da câmera
        offset = new Vector3(0, offsetValues.y, offsetValues.z);
    }
    //LateUpdate funciona depois de todos os outros Updates. Nesse caso é útil pq ele atualiza a posição da camera depois do player se mover pra evitar jittering
    void LateUpdate()
    {
        //Aqui o Update muda a posição da câmera com base na posição do player e somando os valores que a gnt setar
        if(player != null)//Checa se o player existe na cena. Esse if vai ser importante pra quando a gnt criar o script pro player morrer.
        {
            transform.position = player.transform.position + offset;
        }
    }
}
