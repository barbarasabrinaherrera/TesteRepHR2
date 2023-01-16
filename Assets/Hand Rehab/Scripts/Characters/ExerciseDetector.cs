using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseDetector : MonoBehaviour
{
    //public GameObject playerRig; //Não usado???
    //public GameObject leftHandObject; //Não usado???
    public GameObject aim;
    public GameObject shield;
    public GameObject chainLightning; 
    public GameObject boulder;
    public GameObject kamehameha;
    public Camera camera;
    public static List<ExerciseType> availableMagics;

    //LeapProvider provider; //TODO: troca para Oculus
    GameObject sphere;
    GameObject fingerCurlIndicator;
    int fingerCurlNextFinger;
    bool fingerCurlReadyToFire = false;
    bool fingerCurlFired = false;
    Exercise currentExercise;
    GameObject blast;

    float GRAB_TRESHHOLD = 0.063f;
    float ROTATION_UPPER_TRESHHOLD = 3f;
    float ROTATION_LOWER_TRESHHOLD = 0.1f;
    float WRIST_CURL_LOWER_TRESHHOLD = -25f;
    float WRIST_CURL_UPPER_TRESHHOLD = 40f;

    // Start is called before the first frame update
    void Start()
    {
        //provider = FindObjectOfType<LeapProvider>();
        aim = GameObject.Instantiate(aim);
        aim.SetActive(false);
        shield = GameObject.Instantiate(shield, camera.transform);
        shield.SetActive(false);
        currentExercise = new Exercise();
        availableMagics = new List<ExerciseType>();

        availableMagics.Add(ExerciseType.FINGER_CURL);
        availableMagics.Add(ExerciseType.FIST);
        availableMagics.Add(ExerciseType.ROTATION);
        availableMagics.Add(ExerciseType.WRIST_CURL);
    }

    // Update is called once per frame
    void Update()
    {
        //Hand rightHand = null;
        //Hand leftHand = null;
        //Frame frame = provider.CurrentFrame;

        //Passo 1) Pegar a instancia das mãos

        //if (frame.Hands.Capacity > 0)
        //{
        //    foreach (Hand h in frame.Hands)
        //    {
        //        //Debug.Log(h);
        //        if (h.IsLeft)
        //            leftHand = h;
        //        if (h.IsRight)
        //            rightHand = h;
        //    }
        //}

        // Passo 2) Tratar a mão esquerda
        //if (leftHand != null)
        //{

        //TESTE: Assumindo que temos a mão esquerda
        /*if (currentExercise.hasStarted && !currentExercise.hasFinished)
        {
            switch (currentExercise.type)
            {
                case ExerciseType.FIST:
                    ProcessFistExercise();//leftHand);
                    break;
                case ExerciseType.ROTATION:
                    ProcessRotationExercise();//leftHand);
                    break;
                case ExerciseType.WRIST_CURL:
                    ProcessWristCurlExercise();//leftHand);
                    break;
                case ExerciseType.FINGER_CURL:
                    ProcessFingerCurlExercise();//leftHand);
                    break;
            }
        }
        else
        {
            currentExercise.ResetExercise();
            ProcessExercises();//leftHand);
        }*/
    //}




    // Passo 3) Tratar a mão direita
    // Cancel magic
    //if (rightHand != null && currentExercise != null && currentExercise.hasStarted && IsHandClosed(rightHand) && blast == null)
    //{
    //    CancelMagic();
    //}
    }

    //void CancelMagic()
    //{
    //    currentExercise.FinishExercise();
    //    aim?.SetActive(false);
    //    shield?.SetActive(false);
    //    if (fingerCurlIndicator != null)
    //        DestroyImmediate(fingerCurlIndicator);
    //    var magics = GameObject.FindGameObjectsWithTag("Magic");
    //    foreach (var magic in magics)
    //    {
    //        DestroyImmediate(magic);
    //    }
    //}

    //bool IsHandClosed(Hand hand)
    //{
    //    int fingersGrasping = 0;

    //    if (hand != null)
    //    {
    //        Vector3 palm = new Vector3(hand.PalmPosition.x, hand.PalmPosition.y, hand.PalmPosition.z);
    //        foreach (Finger f in hand.Fingers)
    //        {
    //            Vector3 tip = new Vector3(f.TipPosition.x, f.TipPosition.y, f.TipPosition.z);
    //            var dist = Vector3.Distance(tip, palm);
    //            if (dist < GRAB_TRESHHOLD)
    //            {
    //                fingersGrasping++;
    //            }
    //        }
    //    }

    //    return fingersGrasping == 5;
    //}

    //bool IsHandOpened(Hand hand)
    //{
    //    int fingersExtended = 0;

    //    if (hand != null)
    //    {
    //        foreach (Finger f in hand.Fingers)
    //        {
    //            if (f.IsExtended)
    //            {
    //                fingersExtended++;
    //            }
    //        }
    //    }

    //    return fingersExtended == 5;
    //}

    //bool IsHandPointingForward(Hand hand)
    //{
    //    float dot = Vector3.Dot(hand.PalmNormal.ToVector3(), camera.transform.forward);
    //    return dot > 0;
    //}

    bool IsKeyPressed(KeyCode keyCode) {
        bool isPressed = Input.GetKeyDown(keyCode);
        if (isPressed) {
            Debug.Log("Pressed Key: " + keyCode.ToString());
        }
        return isPressed;
    }

    // Aquecimento (Raio)
    void ProcessFistExercise()//Hand hand)
    {
        // Is hand closed
        if (IsKeyPressed(KeyCode.R))//IsHandClosed(hand) && IsHandPointingForward(hand))
        {            
            if (currentExercise.type != ExerciseType.FIST)
            {
                Debug.Log("create sphere");
                currentExercise.StartExercise(ExerciseType.FIST);
                sphere = GameObject.Instantiate(chainLightning);
            }
            //else
            //{
            //    if (sphere?.transform.localScale.x < 0.08)
            //    {
            //        sphere.transform.localScale *= (sphere.transform.localScale.x + 0.001f) / sphere.transform.localScale.x;
            //    }
            //}
        }
        //TEST
        else if (currentExercise.hasStarted && sphere?.transform.localScale.x < 0.08)
        {
            sphere.transform.localScale *= (sphere.transform.localScale.x + 0.001f) / sphere.transform.localScale.x;
        }        
        // Is hand open (after exercise started and sphere is big enough)
        else if (currentExercise.hasStarted && IsKeyPressed(KeyCode.F) &&/*IsHandOpened(hand) && IsHandPointingForward(hand) &&*/ sphere?.transform.localScale.x >= 0.08)
        {
            currentExercise.FinishExercise();
            if (sphere != null)
            {
                Debug.Log("Lança o raio?");
                //Lança o raio?
                //sphere.GetComponent<ChainLightning>().LightItUp(hand);
                //TEST
                var test = GetSpawnPositionInFrontOfPlayer();
                sphere.GetComponent<ChainLightning>().LightItUp(test, test);

                GameObject.DestroyImmediate(sphere);
                sphere = null;
            }
        }

        if (sphere != null)
        {
            //sphere.transform.position = hand.PalmPosition.ToVector3() + hand.PalmNormal.ToVector3().normalized * 0.15f;
            sphere.transform.position = GetSpawnPositionInFrontOfPlayer();
        }
    }
        
    // Prono-supino (Escudo)
    public void ProcessRotationExercise()//Hand hand)
    {
        // Is hand up
        //if (IsKeyPressed(KeyCode.E))//if (!currentExercise.hasStarted && Mathf.Abs(hand.PalmNormal.Roll) > ROTATION_UPPER_TRESHHOLD && IsHandOpened(hand) && !IsHandPointingForward(hand))
        //{
            Debug.Log("ProcessRotationExercise -> Exercicio feito corretamente!");
            //currentExercise.StartExercise(ExerciseType.ROTATION);
            shield.SetActive(true);
        //}
        // Is hand down        
    }

    public void ProcessRotationDownExercice()
    {
        //if (IsKeyPressed(KeyCode.B))//if (currentExercise.hasStarted && Mathf.Abs(hand.PalmNormal.Roll) < ROTATION_LOWER_TRESHHOLD && IsHandOpened(hand) && IsHandPointingForward(hand))
        //{
            //currentExercise.FinishExercise();
            shield.SetActive(false);
            var audios = shield.GetComponents<AudioSource>();
            AudioSource.PlayClipAtPoint(audios[audios.Length - 1].clip, shield.transform.position);
            
        //}
    }

    // Flexão de punho (Pedra)
    void ProcessWristCurlExercise()//Hand hand)
    {
        // Get arm angle
        //float angle = Vector3.SignedAngle(hand.Arm.Direction.ToVector3(), hand.Direction.ToVector3(), hand.RadialAxis());        
        // If has started, show aim target (circulo rosa onde a pedra vai cair)
        if (currentExercise.hasStarted)
        {
            if (aim.transform.localScale.x < 3)
            {
                aim.transform.localScale += new Vector3(0.01f, 0, 0.01f);
            }
            CameraAim();
        }
        // If not started + correct arm angle + hand strength (punho fechado?)
        else if (!currentExercise.hasStarted && IsKeyPressed(KeyCode.P))//&& angle < WRIST_CURL_LOWER_TRESHHOLD && !IsHandPointingForward(hand) && hand.GrabStrength > 0.5)
        {
            currentExercise.StartExercise(ExerciseType.WRIST_CURL);
        }
        //If started + correct arm angle + hand strength + distant (over 3) 
        if (currentExercise.hasStarted && IsKeyPressed(KeyCode.C)/*angle > WRIST_CURL_UPPER_TRESHHOLD && !IsHandPointingForward(hand) && hand.GrabStrength > 0.5*/ && aim?.transform.localScale.x >= 3)
        {
            currentExercise.FinishExercise();
            aim.SetActive(false);
            SummonBoulder(aim);
            aim.transform.localScale = new Vector3(0.1f, 0.05f, 0.1f);
        }
    }

    void CameraAim()
    {
        RaycastHit hit;
        int layerMask = 1 << 8; // Only collides with ground
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            aim.transform.position = hit.point;
            if (!aim.activeSelf)
            {
                Debug.Log("Camera AIM");
                aim.SetActive(true);
            }
        }
        else
        {
            aim.SetActive(false);
        }
    }

    GameObject SummonBoulder(GameObject aim)
    {
        GameObject rock = GameObject.Instantiate(boulder);
        rock.transform.position = aim.transform.position;

        //rock.transform.SetLocalY(25f);
        Vector3 local = rock.transform.localPosition;
        local[1] = 25f;
        rock.transform.localPosition = local;

        float radius = aim.transform.localScale.x;
        rock.transform.localScale *= radius;
        return rock;
    }

    //bool IsFingerPinching(Finger finger)
    //{
    //    Hand hand = provider.CurrentFrame.Hand(finger.HandId);

    //    for (int i = 1; i < hand.Fingers.Count; i++)
    //    {
    //        Finger f = hand.Fingers[i];
    //        if (f.Id != finger.Id && !f.IsExtended)
    //            return false;
    //    }

    //    return (finger.TipPosition - hand.Fingers[0].TipPosition).MagnitudeSquared < 0.0005;
    //}

    Vector3 GetSpawnPositionInFrontOfPlayer() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerPos = player.transform.position;
        Vector3 playerDirection = player.transform.forward;
        Quaternion playerRotation = player.transform.rotation;
        float spawnDistance = 10;
        Vector3 spawnPos = playerPos + playerDirection * spawnDistance;
        return spawnPos;
    }

    // Flexão de dedos (Kamehameha)
    void ProcessFingerCurlExercise()//Hand hand)
    {
        //If not started + index finger pinched
        if (!currentExercise.hasStarted && IsKeyPressed(KeyCode.K))//!IsHandPointingForward(hand) && IsFingerPinching(hand.Fingers[1]))
        {
            currentExercise.StartExercise(ExerciseType.FINGER_CURL);
            fingerCurlIndicator = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            fingerCurlIndicator.GetComponent<Renderer>().material.color = Color.red;
            fingerCurlIndicator.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
            fingerCurlNextFinger = 2;
            fingerCurlReadyToFire = false;
            fingerCurlFired = false;
        }
        // If started + next finger pinched
        else if (currentExercise.hasStarted && !fingerCurlReadyToFire && IsKeyPressed(KeyCode.K))//!IsHandPointingForward(hand) && IsFingerPinching(hand.Fingers[fingerCurlNextFinger]))
        {
            fingerCurlIndicator.transform.localScale += new Vector3(0.02f, 0.02f, 0.02f);
            fingerCurlNextFinger++;
            if (fingerCurlNextFinger == 5)
            {
                fingerCurlReadyToFire = true;
            }
        }

        //TEST
        

        if (fingerCurlIndicator != null)
        {
            //fingerCurlIndicator.transform.position = hand.PalmPosition.ToVector3() + hand.PalmNormal.ToVector3().normalized * 0.15f;
            fingerCurlIndicator.transform.position = GetSpawnPositionInFrontOfPlayer();
        }

        if (fingerCurlReadyToFire && !fingerCurlFired && IsKeyPressed(KeyCode.G))//IsHandPointingForward(hand) && IsHandOpened(hand))
        {
            Destroy(fingerCurlIndicator);
            blast = Instantiate(kamehameha);
            Destroy(blast, 5);
            fingerCurlFired = true;
        }

        if (blast != null)
        {
            //blast.transform.position = hand.PalmPosition.ToVector3() + hand.PalmNormal.ToVector3().normalized * 0.125f;
            //blast.transform.up = hand.PalmNormal.ToVector3();            
            blast.transform.position = GetSpawnPositionInFrontOfPlayer();
        }
        else if (fingerCurlFired)
        {
            currentExercise.FinishExercise();
        }
    }

    void ProcessExercises()//Hand hand)
    {
        //Debug.Log("ProcessExercises: "+availableMagics.Count);
        if (availableMagics.Contains(ExerciseType.FIST)) ProcessFistExercise();//hand);
        if (!currentExercise.hasStarted)
        {
            if (availableMagics.Contains(ExerciseType.ROTATION)) ProcessRotationExercise();//hand);
            if (!currentExercise.hasStarted)
            {
                if (availableMagics.Contains(ExerciseType.WRIST_CURL)) ProcessWristCurlExercise();//hand);
                if (!currentExercise.hasStarted)
                {
                    if (availableMagics.Contains(ExerciseType.FINGER_CURL)) ProcessFingerCurlExercise();//hand);
                }
            }
        }
        //Debug.Log("currentExercise.type:" + currentExercise.type.ToString());
    }
}

//TODO: Criar um arquivo pra cada classe auxiliar
public enum ExerciseType
{
    ROTATION, // Prono-supino (Escudo)
    FIST, // Aquecimento (Raio)
    WRIST_CURL, // Flexão de punho (Pedra)
    FINGER_CURL, // Flexão de dedos (Kamehameha)
    UNDEFINED
}

public class Exercise
{
    public ExerciseType type;
    public bool hasStarted;
    public bool hasFinished;
    public float timeStarted;
    public float timeFinished;

    public Exercise()
    {
        ResetExercise();
    }

    public void StartExercise(ExerciseType type)
    {
        this.type = type;
        this.hasStarted = true;
        this.timeStarted = Time.time;
    }

    public void FinishExercise()
    {
        this.hasFinished = true;
        this.timeFinished = Time.time;
    }

    public void ResetExercise()
    {
        this.type = ExerciseType.UNDEFINED;
        this.hasStarted = false;
        this.hasFinished = false;
        this.timeStarted = -1;
        this.timeFinished = -1;
    }
}