Tasks:
* Spawn animal every 1-2 seconds. Done
* Animals move in random directions by physics. Done
* If they move out of screen they return back. Done
* Two types of animal: prey and predator
    * Predators:
        * When predator collides with other animal, it eats other animal.  Done.
        * When predator eats an animal it shows label "Tasty!". Done
    * Prey:
        * If prey collides with another prey they fly apart. Done.
* Implement two animals:
    * Frog - prey. Every x seconds jump for a fixed distance. TODO: this.
    * Snake - predator. It moves forward. Done.
* Other types of animals are possible.
* UI
    * Have a counter of dead animals - prey and predator. Done.
 
---

Tasks:
* TODO: Pause frog before next jump.
* TODO: Frog should use jump distance not jump force.
* TODO: Refactor TODOs.

Bugs:
* Frogs don't respect horizontal rotation bounds. :/

