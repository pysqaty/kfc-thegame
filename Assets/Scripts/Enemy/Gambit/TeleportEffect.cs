using System.Collections;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class TeleportEffect : MonoBehaviour
{
    [Range(0, 1)]
    public float AnimationTime = 0f;
    [Range(0, 1)]
    public float ParticlePlayTime = 0f;
    [Range(0, 1)]
    public float ParticleReversePlayTime = 1f;
    public float AnimationDuration = 0.5f;
    public bool AnimationDirection = true;

    private float prevAnimTime = 0;

    public Bounds ObjectBounds { get; private set; }
    private Material GetMetarial(Renderer renderer)
    {
        if (Application.isEditor && !Application.isPlaying)
            return renderer.sharedMaterial;
        else
            return renderer.material;
    }

    private List<Renderer> childRenderers;
    private List<ParticleSystem> forwardParticleSystem;
    private List<ParticleSystem> reverseParticleSystem;

    private bool isAnimating = false;

    // Start is called before the first frame update
    void Start()
    {
        childRenderers = GetComponentsInChildren<Renderer>().ToList();

        forwardParticleSystem = GetComponentsInChildren<ParticlePlaybackData>().Where(ppd => ppd.Reverse == false).Select(ppd => ppd.GetComponent<ParticleSystem>()).ToList();
        reverseParticleSystem = GetComponentsInChildren<ParticlePlaybackData>().Where(ppd => ppd.Reverse == true).Select(ppd => ppd.GetComponent<ParticleSystem>()).ToList();

        prevAnimTime = AnimationTime;
    }

    private void UpdateBounds()
    {
        Bounds combinedBounds = childRenderers[0].bounds;
        foreach (Renderer renderer in childRenderers)
        {
            combinedBounds.Encapsulate(renderer.bounds);
        }
        ObjectBounds = combinedBounds;
    }

    private void UpdateMaterialProperties()
    {
        foreach (Renderer renderer in childRenderers)
        {
            GetMetarial(renderer)?.SetFloat("_MinY", ObjectBounds.min.y);
            GetMetarial(renderer)?.SetFloat("_MaxY", ObjectBounds.max.y);

            GetMetarial(renderer)?.SetFloat("_AnimTime", AnimationTime);

            GetMetarial(renderer)?.SetFloat("_InverseDirection", AnimationDirection ? 1 : 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimation();
        UpdateBounds();
        UpdateMaterialProperties();

        if(AnimationTime > ParticlePlayTime && prevAnimTime <= ParticlePlayTime)
        {
            foreach(ParticleSystem ps in forwardParticleSystem)
            {
                ps.Play();
            }
        }

        if (AnimationTime < ParticleReversePlayTime && prevAnimTime >= ParticleReversePlayTime)
        {
            foreach (ParticleSystem ps in reverseParticleSystem)
            {
                ps.Play();
            }
        }

        prevAnimTime = AnimationTime;
    }

    private void UpdateAnimation()
    {
        if(isAnimating == false)
        {
            return;
        }

        if(AnimationDirection)
        {
            AnimationTime += Time.deltaTime / AnimationDuration;
            if(AnimationTime >= 1)
            {
                AnimationTime = 1;
                isAnimating = false;
            }
        }
        else
        {
            AnimationTime -= Time.deltaTime / AnimationDuration;
            if (AnimationTime <= 0)
            {
                AnimationTime = 0;
                isAnimating = false;
            }
        }
    }

    public void Disappear()
    {
        AnimationDirection = true;
        isAnimating = true;
    }

    public void Appear()
    {
        AnimationDirection = false;
        isAnimating = true;
    }
}
