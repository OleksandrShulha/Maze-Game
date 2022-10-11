using UnityEngine;

public class Door : MonoBehaviour
{
    Animator anim;
    int state = 0;
    bool isOpen = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        AnimationDor();
    }

    void AnimationDor()
    {
        if (isOpen == false)
        {
            state = 0;
            anim.SetInteger("State", state);
        }
        else if (isOpen == true)
        {
            state = 1;
            anim.SetInteger("State", state);
        }

    }

    public bool GetDoorIsOpen()
    {
        return isOpen;
    }

    public void SetDoorIsOpen(bool isOpen)
    {
        this.isOpen = isOpen;
    }


}
