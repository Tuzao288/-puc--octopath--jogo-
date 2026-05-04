using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private Rigidbody rb;
    private Vector2 moveInput;
    private Vector2 mouseInput;
    private GameObject score;
    //private GameObject powerUp;
    [SerializeField] private int takePoints;
    public float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Camera cam;

    //Função Awake é sempre chamada antes do Start msm se o script estiver desabilitado ent as atribuições com GetComponent ficam dentro dele
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
    }
    //Função OnMove tem um parâmetro do tipo InputAction.CallbackContext q significa q ele precisa de um input atribuido ao "Action" "Move" lá do InputSystem pra funcionar
    //E atribui o valor do tipo Vetor a variável moveInput
    public void OnMove(InputAction.CallbackContext cntxt)
    {
        moveInput = cntxt.ReadValue<Vector2>();
    }
    //OnLook funciona como o OnMove mas nesse caso ele usa um input de mouse do "Action" "Look"
    public void OnLook(InputAction.CallbackContext cntxt)
    {
        mouseInput = cntxt.ReadValue<Vector2>();
    }
    //No Start a gnt deixa só o "ActionMap" do player ativo por enquanto igual é pedido no aviso do InputPlayer
    private void Start()
    {
        score = GameObject.FindGameObjectWithTag("Canvas");
        //powerUp = GameObject.FindGameObjectWithTag("PowerUp");
        playerInput.actions.FindActionMap("Player").Enable();
        playerInput.actions.FindActionMap("UI").Disable();
    }
    //No FixedUpdate os códigos q rodam independente do framerate
    private void FixedUpdate()
    {
        //Pro movimento é criado um novo Vetor3 q usa os valores dos vetor X e Y do moveInput para dar a direção q o player precisa se mover
        //Multiplica pelo moveSpeed e atribui ao componente de física do player q é o RigidBody
        rb.linearVelocity = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed;

        //Esse comando pega um ponto do mouse na tela e converte para um ponto na visão da camera e atribui para uma variável Ray, que é basicamente uma linha invisivel traçada a partir do ponto
        Ray ray = cam.ScreenPointToRay(mouseInput);
        if (Physics.Raycast(ray, out RaycastHit hit))// Checa se o ray ta batendo em algo com a variável hit. Tem como especificar para ele interagir com áreas especifícas tbm através das layers
        {
            Vector3 targetPoint = hit.point;// atribui o ponto em q o ray está, à variavel targetPoint
            Vector3 direction = targetPoint - transform.position;// Quando dois vetores são subtraídos, o resultado é a direção entre eles
            direction.y = 0;

            if(direction != Vector3.zero)
            {
                Quaternion lookTo = Quaternion.LookRotation(direction);// A direção em q o player vai olhar é atribuído a essa variável lookTo
                rb.rotation = Quaternion.Slerp(rb.rotation, lookTo, rotationSpeed);//Faz a interpolação entre a rotação inicial do player com a direção q ele vai girar e velocidade da rotação
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (score.GetComponent<ScoreScript>().score >= 50)
            {
                score.GetComponent<ScoreScript>().ScoreDown(takePoints);
            }
        }
    }
}
