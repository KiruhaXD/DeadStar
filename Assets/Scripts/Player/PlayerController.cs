using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController _playerController;

    [SerializeField] float _walkSpeed;
    [SerializeField] AudioSource _audioSourceWalk;

    [SerializeField] Transform _groundCheck;
    [SerializeField] float _groundDistance = 0.4f;
    [SerializeField] LayerMask _groundMask;

    [SerializeField] float gravity = -9.81f;
    Vector3 velocity; // скорость падения
    bool isGrounded;

    private void Update()
    {
        Walk();
    }

    public void Walk() 
    {
        isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        Vector3 move = transform.TransformDirection(new Vector3(hor, 0f, ver));

        _playerController.Move(move * _walkSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        _playerController.Move(velocity * Time.deltaTime);

        if (hor > 0 || hor < 0 || ver > 0 || ver < 0) 
        {
            if (_audioSourceWalk.isPlaying) return;
            _audioSourceWalk.Play();
        }

        else
            _audioSourceWalk.Stop();


    }
}
