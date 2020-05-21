using UnityEngine;
using UnityEngine.UI;

public class playermove : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 10f;
    public float gravity = -10f;
    public Transform groundcheck;
    public float grounddistance;
    public LayerMask groundmask;
    public float groundheight=3f;
    public Camera currentCam;
    public int cured_ill_people_number;

    [SerializeField] Camera fpsCamera;
    [SerializeField] Camera tpsCamera;
    [SerializeField] Text ItemInfoText;
    [SerializeField] Canvas InventoryCanvas;
    

    float defaultSpeed;
    Vector3 vel;
    bool isground;
    private void Start()
    {
        defaultSpeed = speed;
        currentCam = tpsCamera;
    }
    void Update()
    {
        PlayerMovementAndJump();
        RaycastDetection();
        PlayerActions();
    }

    private void PlayerMovementAndJump()
    {
        isground = Physics.CheckSphere(groundcheck.position, grounddistance, groundmask);
        if (isground && vel.y < 0)
        {
            vel.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 mov = transform.right * x + transform.forward * z;
        controller.Move(mov * speed * Time.deltaTime);
        if (Input.GetButtonDown("Jump") && isground)
        {
            vel.y = Mathf.Sqrt(groundheight * -2f * gravity);
        }
        vel.y += gravity * Time.deltaTime;
        controller.Move(vel * Time.deltaTime);
    }


    void PlayerActions()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 10;
        }
        else { speed = 5; }

        if (Input.GetKeyDown(KeyCode.I)&&InventoryCanvas.enabled==false)
        {
            InventoryCanvas.enabled=true;//Bunun canvas componentini disabled etdik ki, asagidaki RaycastDetectiondaki GameObject.Find funksiyasini islede bilek.
            Cursor.lockState = CursorLockMode.None;
            transform.parent.GetComponentInChildren<mouselook>().enabled = false;
            controller.enabled = false;
        }
        else if(Input.GetKeyDown(KeyCode.I) && InventoryCanvas.enabled == true)
        {
            InventoryCanvas.enabled = false;
            Cursor.lockState = CursorLockMode.Locked;
            transform.parent.GetComponentInChildren<mouselook>().enabled = true;
            controller.enabled = true;
            
        }
    }

    void RaycastDetection()         //Raycastla obyekt detect etmek ucun istifadə olunur.
    {
        
        RaycastHit DetectionRayHit; //Raycast-gorunmez bir suadir. 1ci parametr - suanin baslama noqtesini, 2ci parametr istiqametini 3cu ise neticeler yazilacaq Raycasti bildirir.
        if (fpsCamera.gameObject.activeSelf)
        {
            currentCam = fpsCamera;
        }
        else
        {
            currentCam = tpsCamera;
        }
        //Eger toxundugu obyekt varsa ve hemin obyekt interactable scripti dasiyirsa
        if (Physics.Raycast(currentCam.transform.position, currentCam.transform.forward, out DetectionRayHit) && DetectionRayHit.collider.GetComponent<Interactable>() != null) //1ci parametr - suanin baslama noqtesini, 2ci parametr istiqametini 3cu ise neticeler yazilacaq Raycasti bildirir.
        {
            if (Vector3.Distance(currentCam.transform.position, DetectionRayHit.collider.gameObject.transform.position) <=8f)
            {
                ItemInfoText.text = "Item:" + DetectionRayHit.transform.name; //Toxundugu obyektin adini text objecte yazsin.
                if (Input.GetKeyDown(KeyCode.Mouse0))                         //Eger oyuncu eyni anda mouse-un sol duymesine basarsa
                {
                    GameObject inventoryGameObject = GameObject.Find(DetectionRayHit.transform.name.ToLower());
                    
                    inventoryGameObject.transform.parent.GetComponentInChildren<Text>().text = (byte.Parse(inventoryGameObject.transform.parent.GetComponentInChildren<Text>().text) + 1).ToString();
                    Destroy(DetectionRayHit.collider.gameObject);//Obyekti mehv etsin.
                }
            }
        }
        else                                //Yox eger toxundugu obyektde interactable scripti yoxdursa
        {
            ItemInfoText.text = "Item:";    //Yazi yerini bos saxlayiriq.
        }

        //if(Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out DetectionRayHit)&&Input.GetKeyDown(KeyCode.Mouse0)&&DetectionRayHit.collider.GetComponent<Interactable>()!=null) //Eger oyuncu eyni anda sol duymeye basarsa(kod optimal deyil , if serti icinde yuxaridaki if(Physics...)de yoxla.
        //{
        //    //Interactable interactable = DetectionRayHit.collider.GetComponent<Interactable>();
        //    Destroy(DetectionRayHit.collider.gameObject);
        //}

    }
}
