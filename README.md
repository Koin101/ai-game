# Infinite Realms

## Instructions
There are two ways to play the game.
1. Clone the github repo and open it using godot 4.3 with C# support. Make sure you add the "LinkToModel" to the models folder. Another model won't work!!
2. Go to the releases on this github page and download the exe file. You will need to manually add a folder at the same location as the exe file named "model" (no quotes). Add the "LinkToModel" to this folder.

------------------------------------------
Infinite Realms is a 2D platformer protoype game which incorporates 3 distinct forms of generative AI. Made using Godot 4 and C#.

1. Image Generation
Within the game we used StableDiffusion to generate level backgrounds, character sprites and animations, and game objects. 

2. Sound Generation
All music and sound effects in the game are AI generated. The music was generated by using Suno and the sound effects were generated using Elevenlabs.

3. Text Generation
We used LLM's for NPC interaction.

Below each aspect is explained in further detail and what exactly is generated within the game.

## Level Design

### First Idea
Our initial idea was based on a video from Game Industry Conference, [How to generate Pixel Perfect 2D levels in Godot 4 with the help of Generative AI - Mat Rowlands](https://youtu.be/1Gw1v1sueFo). 
This video showed how you could create a platformer level using stablediffusion and controlnet. 
The first step is that you build a platform level in godot using a tilemap (preferably a tilemap with a high contrast, so a white tilemap on a black background). Once the level was created using some code it was exported as a png image. One level we created looked like this: ![LevelTemplate](./infinite-realms/templates/template-LevelCreator.png)

Now we can generate a level image using StableDiffusion and ControlNet. 

For specific explanation it is best to watch the video. But it boils down to using txt2img with the generated template in controlnet. All you need to do is think of a theme for your level and hit generate. It ussually takes quite some generation and som tweaking of the settings to get a nice image. Some example levels that we generated are shown below. (these generated using 2 different templates)
![Level1ViaTemplate](./infinite-realms/Assets/LevelBackgrounds/Level1.png)
![Level2ViaTemplate](./infinite-realms/Assets/LevelBackgrounds/Level2.png)
![Level1.2ViaTemplate](./infinite-realms/Assets/LevelBackgrounds/00025-2195908195.png)
![Level1.3ViaTemplate](./infinite-realms/Assets/LevelBackgrounds/00161-3136522623.png)

In the end we did not choose to use this way of generating the levels. As you can see in the levels above there are some visual artifacts and they are a bit bland. It was very hard to create a level with objects in the background. Thus leading to little depth in the image. So we changed gears.

Name exact models used etc.

### Second Idea
We wanted more detail and depth for our levels. So instead of generating an image around the level we turned it around and designed the level after generating an image. We came across a very cool pixel like SD2.1 model named [PIXHELL](https://civitai.com/models/21276/pixhell). The game is played with a resolution of 1920x1080, to make sure we would always have high quality images we generated the image with a resolution of 1920x1080 and upscaled them to 3840x2160. So when the image is displayed in the game it is always of high quality. So for the first level we tried to create a bit of a apocolypse like city, It then generated an image with some platforms, which made it easy for us to to create the whole level. All we did was make add some physics to these platforms which made them walkable for the character. See the image of the level below
![Level1](./infinite-realms/Assets/LevelBackgrounds/FuturisticApocolypsWorld.png)
This was great and in our opinion a better level design then using controlnet. However it is not always easy to get platforms to generate. For level 2 we created a desert like landscape, which as you can imagine usually does not come with some form of platforms. 
For our final level we generated an image of some "alien" like planet with multiple suns, here there were also no platforms generated but there were a lot of rock like structures. So instead of creating our own platforms we just used these rock like structures. Both levels displayed below.
Level 2:
![Level2](./infinite-realms/Assets/LevelBackgrounds/DessertBackground1.png)
Level 3:
![Level3](./infinite-realms/Assets/LevelBackgrounds/WeirdAlienPlanet.png)

The problem with how we made platforms in level 1 and level 3 is that it is difficult to see what are platforms and what are no platforms. Since it is completey blended in with the background. So there is no depth between the platforms and the background. So both methods of generating levels has its pro's and con's. We chose for the second idea since it provided way more interesting level design + the added dificulty of discovering where there are platforms also added to the gameplay.

So for level 2 we still had to create our own platform. While we could have used premade tilesets, we wanted to try and see if we could generate plaforms or some sort of tilemap using StableDiffusion. This time we used img2img where we made a sketch of a platform with some specific colours and then generate an image. 

## Sound Generation
The game includes various sound effects and music tracks to enhance the immersion of the game, all of which were generated using AI.

### Sound Effects
To generate sound effects, the Sound Effect Generator from ELevenLabs was used (https://elevenlabs.io/app/sound-effects/), which allows you to specify the length of the output to create short sounds. The game includes 10 generated sound effects, which are played after the actions of jumping, running, climbing a ladder, talking with an NPC (which uses 5 sounds played randomly), using a portal and opening a chest. The prompts to generate these sounds were created mostly using trial and error, but ChatGPT was also used to get a good prompt for the chest sound. The exact prompts are listed below.
<details>
  <summary>Prompts for the sound effects.</summary>
  
  | Sound Effect  | Prompt |
| ------------- | ------------- |
| Jump | Realistic video game jumping sound from hard surface |
| Run | Futuristic video game running sound on hard surface |
| Portal | Retro video game dimensional portal sound | 
| Npc | Indie video game NPC chatter gibberish male| 
| Ladder | Video game wood ladder climbing sound loop|
| Chest | Generate a sound effect of an old wooden chest creaking open with a metallic latch click|
</details>

### Music
The game includes 5 music tracks which are played throughout the game. These music tracks were generated using the online tool Suno (https://suno.com/), which has a feature to generate instrumental music from a prompt. Since it is quite difficult to describe how you want music to sound and also keep the description within the maximum character limit of 200, ChatGPT was used to generate the prompt for each music piece by feeding it the background image of the level and a short description of its function in the game (e.g., hub world) and these AI generated prompts were then fed directly into Suno, which resulted in very little trial and error and good results. Suno also automatically came up with the name for each music piece. The tracks and prompts are listed below.

<details>
  <summary>Prompts for the music.</summary>
  
| Name  | Location | Prompt |
| ------------- | ------------- | ------------- |
| Pixel Dreamer | Hub (World 1) | Create an upbeat, looping soundtrack for a futuristic, pixel art platformer hub level. The music should combine chiptune melodies with subtle industrial beats and mechanical sound effects. |
|Barren Horizons | Desert (World 2) | Create an atmospheric, looping soundtrack for a pixel art platformer set in a vast, barren desert. The track should blend chiptune elements with ambient desert sounds like wind gusts. |
| Galactic Explorer | Alien Planet (World 3) | Create a looping, alien-themed soundtrack for a pixel art platformer. Use ethereal synths, light percussion, and spacey echoes to evoke mystery and wonder in a floating, otherworldly environment. |
| City Echoes | Main Menu and Intro | Create a looping, ambient track with gritty synths and light percussion for a pixel art post-apocalyptic city. It should evoke exploration and nostalgia, matching the rugged, futuristic setting. |
|Restoration Dawn | End screen | Create a chiptune track with deep synths and light arpeggios, evoking relief and triumph as a futuristic world is restored.|

</details>
