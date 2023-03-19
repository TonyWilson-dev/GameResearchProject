using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AIInterface
{
    void setDestination(Vector3 destination);
    void setSpeed( float speed);
    void setTarget(Vector3 target);
    void useAbility();
}
