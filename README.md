# ModelViewerAssessment
This is a .dae model extention viewer in Unity with simple contols.

## How to Use
* Import all of .dae model files into this directory ("Assets/Resources/Models")
* From the hierarchy in sampleScene, select the "ViewerApp" GameObject. Then write the model name you want to preview in the "AppManager" component in the inspector 
in the "Model Name" field.
* You can choose which to rotate ( Model or the Camera ) for the rotation. select the "ViewerApp" GameObject. you can find a checkbox in the "Controller" component.
check it for Camera rotation. and uncheck it for model rotation.
## How to build
* From menue choos "file" -> Build settings.
* Choose the platform you want and click switch to platform.
* Click Build.

### Build Notes:
* For Android, an apk file will be generated in the desired directory then will be installed on an Android device.
* For IOS, an XCode project will be exported. It should be opened with XCode then build on an IOS device.

## Notes:
* This project is created with Unity version 2020.3.2f1.
* This project can open a model and adjust the camera automatically for initial view.
* There are 2 ways to rotate, a Camera roation and model rotation.
* scroll zoom also added for PC build or player in editor.
* Model provided did not contain materials. And it can be imported with models importing in the future.
* Unity can only import .dae model from models provided. external plugins can be used for importing other models extentions.

#### Thank you
