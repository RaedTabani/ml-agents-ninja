using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using Control;
public class Spawner : MonoBehaviour
{
    [SerializeField] private Obstacle prefab;
    [SerializeField] private int spawnAmount;
    [SerializeField] private int size;
    [SerializeField] private int maxSize;

    private ObjectPool<Obstacle> pool;
    private List<Obstacle> obstacles;
    private void Start()
    {
        obstacles = new List<Obstacle>();
        pool = new ObjectPool<Obstacle>(
            ()=> {return Instantiate(prefab,transform);},
            (obj)=> obj.Reset(),
            (obj)=> obj.gameObject.SetActive(false),
            (obj)=> Destroy(obj),
            false,
            size,
            maxSize
        );

        //Spawn();
    }
    
    public void Spawn(){
        for( int i=0;i<spawnAmount;i++){
            var obstacle = pool.Get();
            obstacle.Init(Kill);
            obstacles.Add(obstacle);
        }
    }
    public void Deactivate(){
        for( int i=0;i<obstacles.Count;i++){
            pool.Release(obstacles[i]);
        }
        obstacles.Clear();
    }
    public void Kill(Obstacle obstacle){
        pool.Release(obstacle);
    }
    
}
