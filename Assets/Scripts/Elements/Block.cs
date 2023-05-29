using UnityEngine;

public class Block : BaseObserver, IObjectPoolItem
{
    [SerializeField]
    MeshRenderer block;

    [SerializeField]
    Material wood;

    [SerializeField]
    Material stone;

    [SerializeField]
    Material glass;

    [SerializeField]
    Rigidbody rb;

    private NetworkExam _info;
    private GradeEnum _grade;
    private int _position;

    public void Set(NetworkExam exam, int position, GradeEnum grade, Transform father)
    {
        rb.useGravity = true;
        rb.isKinematic = true;
       
        transform.SetParent(father);

        _info = exam;
        _position = position;
        _grade = grade;

        block.material = GetMaterial();

        SetScale();
        SetPosition();
        
    }

    private void SetScale()
    {
        block.transform.localScale = new Vector3(GameConstant.HORIZONTAL_SCALE, GameConstant.VERTICAL_SCALE, 1);
    }

    private void SetPosition()
    {
        float y = (_position / 3) * GameConstant.VERTICAL_SCALE;

        if (((_position / 3) % 2) == 0)
        {
            // pair row
            var pos = new Vector3(_position % 3, y, 0);
            var rot = Quaternion.identity * Quaternion.Euler(Vector3.up * 90);

            transform.SetLocalPositionAndRotation(pos, rot);
        }
        else
        {
            // odd row
            var pos = new Vector3(1, y, (_position % 3) - 1);
            var rot = Quaternion.identity;

            transform.SetLocalPositionAndRotation(pos, rot);
        }

    }

    private Material GetMaterial()
    {
        return _info.mastery switch
        {
            2 => stone,
            1 => wood,
            _ => glass,
        };
    }

    private void OnMouseUpAsButton()
    {
        Notify(GameEventEnum.EXAM_SELECTED, new object[] { _info });
    }

    protected override void OnExamSelectedEvent(NetworkExam exam)
    {
        base.OnExamSelectedEvent(exam);

        if (_info.Equals(exam))
            block.material.SetColor("_Color", Color.red);
        else
            block.material.SetColor("_Color", _info.mastery == 0 ? Color.cyan : Color.white);
    }

    protected override void OnTestStackEvent(GradeEnum grade)
    {
        if(grade == _grade)
        {
            rb.useGravity = true;
            rb.isKinematic = false;

            if(_info.mastery == 0)
            {
                ReturnToPool();
            }
        }
    }


    #region Pool item

    public bool _enabled;
    IObjectPool<IObjectPoolItem> pool;

    public void SetPool(IObjectPool<IObjectPoolItem> p)
    {
        pool = p;
    }

    public void ReturnToPool()
    {
        pool.Return(this);
    }

    public void Enable()
    {
        _enabled = true;
    }

    public void Reset()
    {
        _enabled = false;
        transform.localPosition = new Vector3(Screen.width * 3, 0);
    }


    #endregion


}
