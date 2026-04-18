using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [Header("ID")]
    [SerializeField] private int playerId;
    [SerializeField] private bool isLocalPlayer = false;

    [Header("Movement")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Camera cam;
    [SerializeField] private float moveDistance = 2f;

    [Header("Visual")]
    [SerializeField] private Transform arrow;

    private AudioListener audioListener;

    void Start()
    {
        if (agent != null)
        {
            agent.autoBraking = false;
        }

        if (cam != null)
        {
            audioListener = cam.GetComponent<AudioListener>();

            if (audioListener != null)
            {
                audioListener.enabled = false;
            }

            cam.gameObject.SetActive(true);
        }
    }

    public void SetupPlayer(int localId)
    {
        isLocalPlayer = (playerId == localId);

        if (cam != null)
        {
            cam.gameObject.SetActive(isLocalPlayer); 
        }

        if (audioListener != null)
        {
            audioListener.enabled = isLocalPlayer;
        }
    }

    void Update()
    {
        
        if (!isLocalPlayer) return;

        HandleMovement();
    }

    void HandleMovement()
    {
        if (cam == null) return;
        {
            cam.gameObject.SetActive(isLocalPlayer);
        }

        if (!Input.GetMouseButtonDown(0)) return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 direction = (hit.point - transform.position).normalized;
            Vector3 target = transform.position + direction * moveDistance;

            if (arrow != null && direction != Vector3.zero)
            {
                arrow.forward = direction;
            }

            agent.SetDestination(target);
        }
    }

    
    public void MovePlayer(Vector3 position)
    {
        if (isLocalPlayer) return;

        if (agent != null)
        {
            agent.SetDestination(position);
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public int GetPlayerId()
    {
        return playerId;
    }
}