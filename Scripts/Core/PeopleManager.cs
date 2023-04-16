using UnityEngine;

namespace Game.Core
{
    public class PeopleManager : MonoBehaviour
    {
        [SerializeField] VariableInt people;
        [SerializeField] VariableInt houses;
        [SerializeField] VariableInt withoutFood;
        [SerializeField] VariableInt withoutHouses;
        [SerializeField] CurrencyModificator peopleGet;
        [SerializeField] CurrencyModificator peopleGive;

        int enpughtGetFor;

        private void OnEnable()
        {
            EventsManager.OnTick += PeopleUpdate;
        }

        private void OnDisable()
        {
            EventsManager.OnTick -= PeopleUpdate;
        }

        void PeopleUpdate()
        {
            if (peopleGet.Currency.IsEnaughtCurrency(Mathf.Abs(peopleGet.AmountPerTickVariable.Value) * people.Value))
            {
                peopleGet.ManualUpdate(people.Value);
                peopleGive.ManualUpdate(people.Value);
                withoutFood.UpdateValue(0);
            }
            else
                withoutFood.UpdateValue(people.Value);

            if (houses.Value < people.Value)
            {
                withoutHouses.UpdateValue(people.Value - houses.Value);
            }
            else
                withoutHouses.UpdateValue(0);
        }
    }
}