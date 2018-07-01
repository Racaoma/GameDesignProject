using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    //Controller References
    private CursorChangerController cursorChangerController;
    private SpellEffectController spellEffectController;
    private ManaController manaController;
    private ScreenShakeController screenShakeController;
    private DevourerController devourerController;
    private ScreenFlashController screenFlashController;
    private CorruptionController corruptionController;
    private EnvironmentController environmentController;
    private ConditionController conditionController;
    private GrimmoireController grimmoireController;
    private EnemySpawner enemySpawner;

    //Singleton Instance Variable
    private static ControllerManager instance;
    public static ControllerManager Instance
    {
        get
        {
            return instance;
        }
    }

    //On Object Awake
    private void Awake()
    {
        //Check Singleton
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    //On Object Destroy (Safeguard)
    public void OnDestroy()
    {
        instance = null;
    }

    //Start Method
    private void Start()
    {
        cursorChangerController = this.GetComponent<CursorChangerController>();
        spellEffectController = this.GetComponent<SpellEffectController>();
        manaController = this.GetComponent<ManaController>();
        screenShakeController = this.GetComponent<ScreenShakeController>();
        devourerController = this.GetComponent<DevourerController>();
        screenFlashController = this.GetComponent<ScreenFlashController>();
        corruptionController = this.GetComponent<CorruptionController>();
        environmentController = this.GetComponent<EnvironmentController>();
        conditionController = this.GetComponent<ConditionController>();
        grimmoireController = this.GetComponent<GrimmoireController>();
        enemySpawner = this.GetComponent<EnemySpawner>();
    }

    //Get Methods
    public CursorChangerController getCursorChangerController()
    {
        return cursorChangerController;
    }

    public SpellEffectController getSpellEffectController()
    {
        return spellEffectController;
    }

    public ManaController getManaController()
    {
        return manaController;
    }

    public ScreenShakeController getScreenShakeController()
    {
        return screenShakeController;
    }

    public DevourerController getDevourerController()
    {
        return devourerController;
    }

    public ScreenFlashController getScreenFlashController()
    {
        return screenFlashController;
    }

    public CorruptionController getCorruptionController()
    {
        return corruptionController;
    }

    public EnvironmentController getEnvironmentController()
    {
        return environmentController;
    }

    public ConditionController getConditionController()
    {
        return conditionController;
    }

    public GrimmoireController getGrimmoireController()
    {
        return grimmoireController;
    }

    public EnemySpawner getEnemySpawner()
    {
        return enemySpawner;
    }
}
