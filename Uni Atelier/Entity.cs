using UnityEngine;

public class Entity : MonoBehaviour
{
    public Animator anim { get; private set; }
    public Rigidbody rbody { get; private set; }

    Player player;

    Enemy enemy;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
        enemy = GetComponent<Enemy>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

    public void ZeroVelocity()
    {
        rbody.velocity = Vector2.zero;
    }

    public void SetVelocity(float _xVelocity, float _zVelocity, float _rotateSpeed)
    {
        Vector3 inputDir = new Vector3(_xVelocity, 0, _zVelocity);
        rbody.MovePosition(transform.position + inputDir * Time.fixedDeltaTime);
        transform.forward = Vector3.Lerp(transform.forward, inputDir,
            _rotateSpeed * Time.fixedDeltaTime);
    }
}
