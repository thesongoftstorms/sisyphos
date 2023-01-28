using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSize : MonoBehaviour
{
  public float growDirection = 1;
  public float turningPoint;
  public float footOfHill;
  public float growthFactor;
  private Rigidbody2D body;
  private float origSizeX;
  private float origSizeZ;
  private float currentSize;
  private float topSize;
  private float upWardOrDownWard = 0;
  private float origMass;
  
  void Start() {
    body = GetComponent<Rigidbody2D>();
    origSizeX = transform.localScale.x;
    origSizeZ = transform.localScale.z;
    origMass = body.mass;
    topSize = origSizeX + (float)((turningPoint-footOfHill)*growthFactor*growDirection); 
    currentSize = origSizeX;

  }

  private bool isBehindTurningPoint() {
    return this.body.position.x >= this.turningPoint;
  }

  private bool returnedAfterRoll() {
    return this.body.position.x <= this.footOfHill;
  }

  private bool isInSlope() {
    return this.body.position.x > this.footOfHill;
  }

  private bool rollsUp() {
    return this.upWardOrDownWard != -1;
  }
  
  private bool rollsDown() {
    return this.upWardOrDownWard == -1;
  }

  private void setScale() {
      transform.localScale = new Vector3(this.currentSize, this.currentSize, this.origSizeZ);
  }

  void Update() {
    this.body.mass = origMass;
    
    if (this.isInSlope()) {
        float newMass = (float)(Mathf.Exp(this.body.mass * this.body.position.x) * this.growDirection);
        newMass = Mathf.Clamp(newMass, -origMass*4, (float)(origMass*2.05));
        this.body.mass += newMass;
    }
    
    if (this.isBehindTurningPoint()) {
      this.upWardOrDownWard = -1;
    }
    if (this.rollsUp() && this.isInSlope() && !this.isBehindTurningPoint()) {
      this.upWardOrDownWard = 1;
      this.currentSize = this.origSizeX + (float)((this.body.position.x - this.footOfHill)*this.growthFactor)*this.growDirection; // +1 to correct X offset
    }
    
    if (this.rollsDown() && this.isInSlope() && !this.isBehindTurningPoint()) {
        this.currentSize = this.topSize - (float)((this.turningPoint - this.body.position.x)*this.growthFactor*this.growDirection);
    }
    if (this.returnedAfterRoll()) {
        this.upWardOrDownWard = 0;
        this.currentSize = this.origSizeX;
    }
    this.setScale();
  }
}
