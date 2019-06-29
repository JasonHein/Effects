# Effects
A collection of Unity scripts for playing, triggering, and controlling effects. Handles particles, sounds, and animations with the potential to be used in object pooling for shared effects. Includes an object pooling script.


# Overview of Effect Scripts

In general, the effect's scripts are a wrapper for common effects. So for example they might tell a particle effect script to play.

The draw of this is that because Effect is an abstract class all the other effects inherit from we can use all of them in a quick manager script called Effect Controller.

When you play or stop an effect controller, it gives that command to all of the effects on child objects.

I've also added some helper scripts so when you hit play you can specify a location or an object in the scene to follow.


# Object Pooling

For details on the object pooling script please look at the read me here. https://github.com/JasonHein/Object-Pooling/blob/master/README.md

All effects inherit from an interface called Poolable, so any object with an effects or effect controller script attached can be pooled.
