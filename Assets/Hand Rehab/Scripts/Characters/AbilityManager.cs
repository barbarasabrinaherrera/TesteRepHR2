using System.Collections;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject leftHand;
    public GameObject leftHandAnchor;
    public GameObject rightHand;
    public GameObject rightHandAnchor;

    //Powers
    //Shield / Prono Supino (Paper)
    public GameObject shield;

    //Lightning / Aquecimento (Mão fechada/Rock)
    [SerializeField] GameObject chargeLightningMagicObj;
    [SerializeField] GameObject attackLightningMagicObj;

    //??/ Flexão de punho
    [SerializeField] GameObject chargeFireMagicObj;
    [SerializeField] GameObject attackGreenLightMagicObj;

    //Ice / Flexão de dedos (Pinch)
    [SerializeField] GameObject chargeIceMagicPinchObj;
    [SerializeField] GameObject attackIceMagicObj;
    
    // Start is called before the first frame update
    void Start()
    {
        //Instanciate Shield Magic
        InstantiateGameObjectAndSetInactive(ref shield, playerCamera.transform);
        
        //Instanciate Lightning Magic object
        //Left Hand
        InstantiateGameObjectAndSetInactive(ref chargeLightningMagicObj, leftHandAnchor.transform);
        InstantiateGameObjectAndSetInactive(ref attackLightningMagicObj, leftHandAnchor.transform);
        //Right Hand
        InstantiateGameObjectAndSetInactive(ref chargeLightningMagicObj, rightHandAnchor.transform);
        InstantiateGameObjectAndSetInactive(ref attackLightningMagicObj, rightHandAnchor.transform);

        //Instanciate Fire and Green LIght Magic
        //Left Hand
        InstantiateGameObjectAndSetInactive(ref chargeFireMagicObj, leftHandAnchor.transform);
        InstantiateGameObjectAndSetInactive(ref attackGreenLightMagicObj, leftHandAnchor.transform);
        //Right Hand
        InstantiateGameObjectAndSetInactive(ref chargeFireMagicObj, rightHandAnchor.transform);
        InstantiateGameObjectAndSetInactive(ref attackGreenLightMagicObj, rightHandAnchor.transform);

        //Instanciate Ice Magic
        //Left Hand
        InstantiateGameObjectAndSetInactive(ref chargeIceMagicPinchObj, leftHandAnchor.transform);
        InstantiateGameObjectAndSetInactive(ref attackIceMagicObj, leftHandAnchor.transform);
        //Right Hand
        InstantiateGameObjectAndSetInactive(ref chargeIceMagicPinchObj, rightHandAnchor.transform);
        InstantiateGameObjectAndSetInactive(ref attackIceMagicObj, rightHandAnchor.transform);

    }

    private void InstantiateGameObjectAndSetInactive(ref GameObject gameObject, Transform spawnTransform)
    {
        gameObject = Instantiate(gameObject, spawnTransform);
        gameObject.SetActive(false);
    }

    private void DebugHandPosition(GameObject hand)
    {
        Debug.Log("Tranform Position: " + hand.transform.position);
        Debug.Log("Tranform Rotation: " + hand.transform.rotation);
    }

    float _lightningTimer = 0;    
    float _waitBeforeDeactivateShieldTimer = 0;
    public bool isShieldActive = false;

    bool _isChargingLightning = false;

    float _greenLightTimer = 0;
    bool _isChargingGreenLight = false;
    bool _isLeftFireCharged = false;
    bool _isRightFireCharged = false;

    float _waitBeforeIceAttackTimer = 0;
    //Left Hand
    bool _isLeftIndexPinched = false;
    bool _isLeftMiddlePinched = false;
    bool _isLeftRingPinched = false;
    bool _isLeftPinkyPinched = false;
    //Right Hand
    bool _isRightIndexPinched = false;
    bool _isRightMiddlePinched = false;
    bool _isRightRingPinched = false;
    bool _isRightPinkyPinched = false;

    private bool isLeftHandActivatingEffect;
    private bool isRightHandActivatingEffect;

    void Update()
    {
        if(isShieldActive)
        {
            _waitBeforeDeactivateShieldTimer += Time.deltaTime;
        }

        TestIfShieldCanDeactivate();

        if (_isChargingLightning)
        {
            _lightningTimer += Time.deltaTime;
        }
        //Test if i should activate Lightning Spark
        TestIfLightningAttackActivate();

        if (_isChargingGreenLight)
        {
            _greenLightTimer += Time.deltaTime;
        }

        TestIfGreenLightActivate();

        if (CanActivateIceAttack())
        {
            _waitBeforeIceAttackTimer += Time.deltaTime;
        }
        //Test if i should activate Ice attack
        TestIfIceAttackActivate();
    }

    public void ShieldActivation()
    {
        shield.SetActive(true);
        isShieldActive = true;
    }

    public void ShieldDeactivation()
    {
        shield.SetActive(false);
        isShieldActive = false;
    }
    private void TestIfShieldCanDeactivate()
    {
        if (isShieldActive)
        {
            const float ChargeTime = 2f; //Hold for x seconds
            if (_waitBeforeDeactivateShieldTimer >= ChargeTime)
            {
                ShieldDeactivation();
                _waitBeforeDeactivateShieldTimer = 0;
            }
        }
    }

    private void SetGameObjectToHandAndActive(ref GameObject gameObject, GameObject hand)
    {
        gameObject.transform.position = hand.transform.position;
        gameObject.transform.rotation = hand.transform.rotation;
        gameObject.SetActive(true);
    }

    private void SetGameObjectInactive(ref GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public void LeftHandActivating()
    {
        isLeftHandActivatingEffect = true;
    }
        public void RightHandActivating()
    {
        isRightHandActivatingEffect = true;
    }
    public void LightningMagicActivation()
    {
        _lightningTimer = 0;
        _isChargingLightning = true;
        if (isLeftHandActivatingEffect)
        {
            SetGameObjectToHandAndActive(ref chargeLightningMagicObj, leftHandAnchor);            
        }
        else if(isRightHandActivatingEffect)
        {
            SetGameObjectToHandAndActive(ref chargeLightningMagicObj, rightHandAnchor);
        }
    }

    public void LightningMagicDeactivation()
    {
        _lightningTimer = 0;
        _isChargingLightning = false;
        SetGameObjectInactive(ref chargeLightningMagicObj);
    }

    private void TestIfLightningAttackActivate()
    {
        if (_isChargingLightning)
        {
            const float ChargeTime = 2f; //Hold for x seconds
            if (_lightningTimer >= ChargeTime)
            {
                SetGameObjectInactive(ref chargeLightningMagicObj);
                if (isLeftHandActivatingEffect)
                { 
                    SetGameObjectToHandAndActive(ref attackLightningMagicObj, leftHandAnchor);
                }
                else if (isRightHandActivatingEffect)
                {
                    SetGameObjectToHandAndActive(ref attackLightningMagicObj, rightHandAnchor);
                }
                StartCoroutine(ResetLightning());
                DamageAllEnemies(0.5f);
            }
        }
    }

    IEnumerator ResetLightning()
    {
        const float SecondsToWait = 2f;
        yield return new WaitForSeconds(SecondsToWait);        
        //Reset after x seconds
        LightningMagicDeactivation();
        if (isLeftHandActivatingEffect)
            isLeftHandActivatingEffect = false;//set false after activation is on
        else if (isRightHandActivatingEffect)
            isRightHandActivatingEffect = false;//set false after activation is on
        SetGameObjectInactive(ref attackLightningMagicObj);
    }

    public void FireMagicActivation()
    {
        if (isLeftHandActivatingEffect)
        {
            _isLeftFireCharged = true;
            SetGameObjectToHandAndActive(ref chargeFireMagicObj, leftHandAnchor);
            isLeftHandActivatingEffect = false;
        }
        else if (isRightHandActivatingEffect)
        {
            _isRightFireCharged = true;
            SetGameObjectToHandAndActive(ref chargeFireMagicObj, rightHandAnchor);
            isRightHandActivatingEffect = false;
        }

    }
        public void FireMagicDeactivation()
    {
        SetGameObjectInactive(ref chargeFireMagicObj);
    }

    public void GreenLightMagicActivation()
    {
        if(isLeftHandActivatingEffect && _isLeftFireCharged)
        {
            SetGameObjectToHandAndActive(ref attackGreenLightMagicObj, leftHandAnchor);
            _isChargingGreenLight = true;
            isLeftHandActivatingEffect = false;
        }
        else if(isRightHandActivatingEffect && _isRightFireCharged)
        {
            SetGameObjectToHandAndActive(ref attackGreenLightMagicObj, rightHandAnchor);
            _isChargingGreenLight = true;
            isRightHandActivatingEffect = false;
        }
    }
    public void GreenLightMagicDeactivation()
    {
        SetGameObjectInactive(ref attackGreenLightMagicObj);
        _isChargingGreenLight = false;
        _greenLightTimer = 0;
    }
    private void TestIfGreenLightActivate()
    {
        if (_isChargingGreenLight)
        {
            const float ChargeTime = 1f; //Hold Pose for x seconds
            if (_greenLightTimer >= ChargeTime)
            {
                DamageAllEnemies(0.75f);
                if(_isLeftFireCharged)
                    _isLeftFireCharged = false;
                else if(_isRightFireCharged)
                    _isRightFireCharged = false;
            }
        }
    }

    public void IceMagicActivation() {
        SetGameObjectToHandAndActive(ref chargeIceMagicPinchObj, leftHandAnchor);
    }

    public void IceMagicDeactivation()
    {
        SetGameObjectInactive(ref chargeIceMagicPinchObj);
    }

    public void IndexFingerPinch()
    {
        if (isLeftHandActivatingEffect)
        {
            _isLeftIndexPinched = true;
            isLeftHandActivatingEffect = false;
        }
        else if (isRightHandActivatingEffect)
        {
            _isRightIndexPinched = true;
            isRightHandActivatingEffect = false;
        }
    }

    public void MiddleFingerPinch()
    {
        if (isLeftHandActivatingEffect)
        {
            _isLeftMiddlePinched = true;
            isLeftHandActivatingEffect = false;
        }
        else if (isRightHandActivatingEffect)
        {
            _isRightMiddlePinched = true;
            isRightHandActivatingEffect = false;
        }
        
    }

    public void RingFingerPinch()
    {
        if (isLeftHandActivatingEffect)
        {                        
            _isLeftRingPinched = true;
            isLeftHandActivatingEffect = false;
        }
        else if (isRightHandActivatingEffect)
        {
            _isRightRingPinched = true;
            isRightHandActivatingEffect= false;
        }        
    }

    public void PinkyFingerPinch()
    {
        if (isLeftHandActivatingEffect)
        {
            _isLeftPinkyPinched = true;
        }
        else if (isRightHandActivatingEffect)
        {
            _isRightPinkyPinched = true;
        }        
    }

    private bool CanActivateIceAttack() 
    {
        if (isLeftHandActivatingEffect)
        {
            if (_isLeftIndexPinched && _isLeftMiddlePinched && _isLeftRingPinched && _isLeftPinkyPinched)
                return true;
        }
        else if (isRightHandActivatingEffect)
        {
            if (_isRightIndexPinched && _isRightMiddlePinched && _isRightRingPinched && _isRightPinkyPinched)
                return true;
        }
        return false;
    }

    private void TestIfIceAttackActivate()
    {
        if (CanActivateIceAttack())
        {
            const float WaitTime = 0.5f; //Wait for x seconds
            if (_waitBeforeIceAttackTimer >= WaitTime)
            {
                if (isLeftHandActivatingEffect)
                {
                    SetGameObjectToHandAndActive(ref attackIceMagicObj, leftHandAnchor);
                }
                else if(isRightHandActivatingEffect)
                {
                    SetGameObjectToHandAndActive(ref attackIceMagicObj, rightHandAnchor);
                }
                SetGameObjectInactive(ref chargeIceMagicPinchObj);
                StartCoroutine(ResetPinchFingers());
                DamageAllEnemies(1);
            }
        }
    }
    IEnumerator ResetPinchFingers() 
    {
        const float SecondsToWait = 4f;
        yield return new WaitForSeconds(SecondsToWait);
        //Reset after 2 seconds
        _waitBeforeIceAttackTimer = 0;
        SetGameObjectInactive(ref attackIceMagicObj);
        if (isLeftHandActivatingEffect)
        {
            _isLeftIndexPinched = false;
            _isLeftMiddlePinched = false;
            _isLeftRingPinched = false;
            _isLeftPinkyPinched = false;
            isLeftHandActivatingEffect = false;
        }
        else if (isRightHandActivatingEffect)
        {
            _isRightIndexPinched = false;
            _isRightMiddlePinched = false;
            _isRightRingPinched = false;
            _isRightPinkyPinched = false;
            isRightHandActivatingEffect = false;
        }
    }

    public void TurnOffAllMAgics()
    {
        ShieldDeactivation();
        LightningMagicDeactivation();
        SetGameObjectInactive(ref attackLightningMagicObj);
        IceMagicDeactivation();
        SetGameObjectInactive(ref attackIceMagicObj);
        GreenLightMagicDeactivation();
        FireMagicDeactivation();
    }

    //TODO: Put in other script
    private void DamageAllEnemies(float damage)
    {
        GameObject[] allEnemies;
        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in allEnemies)
        {
            Character character = enemy.GetComponent<Character>();
            character.Hit(damage, Element.NORMAL);
        }
    }
}
