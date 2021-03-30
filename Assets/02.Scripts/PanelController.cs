using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    private int hashIsButtonClicked;
    public bool isOperated = false;

    private int hashAloneMode;

    void Awake()
    {
        hashIsButtonClicked = Animator.StringToHash("isButtonClicked");
        hashAloneMode = Animator.StringToHash("IsAloneModeClicked");
    }

    public void ChangeHashValue(bool value)
    {
        Debug.Log($"PanelController ::: {value}");
        isOperated = value;
        animator.SetBool(hashIsButtonClicked, value);
        Debug.Log($"{this.gameObject.name} ::: {isOperated}");
    }

    public void SelectAloneModePanel(bool value)
    {
        Debug.Log($"PanelController ::: {this.gameObject.name} hashAloneMode // {value}");
        isOperated = value;
        animator.SetBool(hashAloneMode, value);
    }
}
