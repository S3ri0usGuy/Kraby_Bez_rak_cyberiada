using UnityEngine;

public interface ITriggerCheckable
{
   bool isAggroed { get; set; }
   bool isWithinStrikingDistance { get; set; }

    void setAggroStatus(bool isAggroed);
    void SetStrikingDistanceBool(bool isWithinStrikingDistance);
}
