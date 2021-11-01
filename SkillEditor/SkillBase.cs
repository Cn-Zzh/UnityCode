using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase
{
    public string name = string.Empty;
    public float time = 0;
    public virtual void Trigger()
    {

    }
    public virtual void Play()
    {

    }
    public virtual void Init()
    {

    }
    public virtual void Stop()
    {

    }
    public virtual void Update()
    {

    }

}

public class Skill_Effects : SkillBase
{
    public GameObject gameClip;
    Player player;

    ParticleSystem particleSystem;
    GameObject obj;
    float AllTime = 0;
    public Skill_Effects(Player _player)
    {
        player = _player;
    }
    public override void Trigger()
    {
        base.Trigger();
        if (time == 0)
        {
            Play();
        }
        else
        {
            AllTime = time;
        }
    }
    public void SetGameClip(GameObject _audioClip, float time)
    {
        gameClip = _audioClip;
        this.time = time;
        if (gameClip.GetComponent<ParticleSystem>())
        {
            obj = GameObject.Instantiate(gameClip, player.effectsparent);
            particleSystem = obj.GetComponent<ParticleSystem>();
            particleSystem.Stop();
        }
        name = _audioClip.name;
    }
    public override void Play()
    {
        base.Play();
        if (particleSystem != null)
        {
            particleSystem.Play();
        }
    }
    public override void Init()
    {
        if (gameClip.GetComponent<ParticleSystem>())
        {
            particleSystem = obj.GetComponent<ParticleSystem>();
            particleSystem.Stop();
        }
    }
    public override void Stop()
    {
        base.Play();
        if (particleSystem != null)
            particleSystem.Stop();
    }
    public override void Update()
    {
        base.Update();
        if (AllTime > 0)
        {
            AllTime = AllTime - Time.deltaTime;
            if (AllTime <= 0)
            {
                Play();
                AllTime = 0;
            }
        }
    }
}

public class Skill_Audio : SkillBase
{

    Player player;

    AudioSource audioSource;

    public AudioClip audioClip;
    float AllTime = 0;

    public Skill_Audio(Player _player)
    {
        player = _player;
        audioSource = player.gameObject.GetComponent<AudioSource>();
    }
    public override void Update()
    {
        base.Update();
        if (AllTime > 0)
        {
            AllTime = AllTime - Time.deltaTime;
            if (AllTime <= 0)
            {
                Play();
                AllTime = 0;
            }
        }
    }
    public override void Trigger()
    {
        base.Trigger();
        if (time == 0)
        {
            Play();
        }
        else
        {
            AllTime = time;
        }
    }
    public void SetAnimClip(AudioClip _audioClip, float time)
    {
        audioClip = _audioClip;
        this.time = time;
        name = audioClip.name;
    }
    public override void Play()
    {
        base.Play();
        audioSource.clip = audioClip;
        audioSource.Play();
    }
    public override void Init()
    {
        audioSource.clip = audioClip;
    }
    public override void Stop()
    {
        base.Play();
        audioSource.Stop();
    }
}

public class Skill_Anim : SkillBase
{
    Player player;

    Animator anim;

    public AnimationClip animClip;
    AnimatorOverrideController controller;

    float AllTime = 0;

    public Skill_Anim(Player _player)
    {
        player = _player;
        anim = player.gameObject.GetComponent<Animator>();
        controller = player.overrideController;
    }
    public override void Trigger()
    {
        base.Trigger();
        if (time == 0)
        {
            Play();
        }
        else
        {
            AllTime = time;
        }
    }
    public override void Update()
    {
        base.Update();
        if (AllTime > 0)
        {
            AllTime = AllTime - Time.deltaTime;
            if (AllTime <= 0)
            {
                Play();
                AllTime = 0;
            }
        }
    }
    public override void Init()
    {
        controller["Start"] = animClip;
    }
    public void SetAnimClip(AnimationClip _animClip, float time)
    {
        animClip = _animClip;
        this.time = time;
        name = animClip.name;
    }
    public override void Play()
    {
        base.Play();
        controller["Start"] = animClip;
        anim.StopPlayback();
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Idle1"))
        {
            anim.SetTrigger("Play");
        }
    }
    public override void Stop()
    {
        base.Play();
        anim.StartPlayback();
    }
}
