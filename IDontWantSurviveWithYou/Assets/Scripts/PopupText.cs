using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupText : MonoBehaviour
{
    public static void Create(Vector3 position, int damageAmount, bool isCriticalHit)
    {
        PopupText popupText = Instantiate(ResourseManager.instance.popupText ,position , Quaternion.identity);
        popupText.Setup(damageAmount, isCriticalHit);
    }

    private TextMeshPro _textMeshPro;
    private Color _textColor;

    [Header("Move Up")]
    public Vector3 moveUpVector = new Vector3(0.3f, 1.0f, 0);
    public float moveUpSpeed = 3.0f;
    public float scaleSpeed = 1.5f;
    [Header("Move Down")]
    public Vector3 moveDownVector = new Vector3(-0.7f, 1.0f, 0);

    [Header("Disappear")]
    private const float DisappearTimeMax = 0.2f;
    private float _disappearTimer;
    public float disappearSpeed = 3.0f;

    [Header("Damage Color")]
    public Color normalColor;
    public Color criticalHitColor;
    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshPro>();
    }
    public void Setup(int damageAmount, bool isCriticalHit)
    {
        _textMeshPro.SetText(damageAmount.ToString());
        if(isCriticalHit)
        {
            _textMeshPro.fontSize = 10;
            _textColor = criticalHitColor;
        }
        else
        {
            _textMeshPro.fontSize = 6;
            _textColor = normalColor;
        }
        _textMeshPro.color = _textColor;
        _disappearTimer = DisappearTimeMax;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += moveUpVector * Time.deltaTime;
        moveUpVector += moveUpVector * (Time.deltaTime * moveUpSpeed);
        /*
        //move up
        if (_disappearTimer > DisappearTimeMax * 0.5f)
        {
            transform.position += moveUpVector * Time.deltaTime;
            moveUpVector += moveUpVector * (Time.deltaTime * moveUpSpeed);
            transform.localScale += Vector3.one * (Time.deltaTime * scaleSpeed);
        }
        else
        {
            //move down
            transform.position -= moveDownVector * Time.deltaTime;
            transform.localScale -= Vector3.one * (Time.deltaTime * scaleSpeed);
        }
        */


        // disappear
        _disappearTimer -= Time.deltaTime;
        if( _disappearTimer < 0 )
        {
            _textColor.a -= disappearSpeed * Time.deltaTime;
            _textMeshPro.color = _textColor;
            if (_textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
