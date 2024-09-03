using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public static class HumanoidUtils
{
    // 3D joint skeleton
    enum JointIndices
    {
        Invalid = -1,
        Root = 0, // parent: <none> [-1]
        Hips = 1, // parent: Root [0]
        LeftUpLeg = 2, // parent: Hips [1]
        LeftLeg = 3, // parent: LeftUpLeg [2]
        LeftFoot = 4, // parent: LeftLeg [3]
        LeftToes = 5, // parent: LeftFoot [4]
        LeftToesEnd = 6, // parent: LeftToes [5]
        RightUpLeg = 7, // parent: Hips [1]
        RightLeg = 8, // parent: RightUpLeg [7]
        RightFoot = 9, // parent: RightLeg [8]
        RightToes = 10, // parent: RightFoot [9]
        RightToesEnd = 11, // parent: RightToes [10]
        Spine1 = 12, // parent: Hips [1]
        Spine2 = 13, // parent: Spine1 [12]
        Spine3 = 14, // parent: Spine2 [13]
        Spine4 = 15, // parent: Spine3 [14]
        Spine5 = 16, // parent: Spine4 [15]
        Spine6 = 17, // parent: Spine5 [16]
        Spine7 = 18, // parent: Spine6 [17]
        LeftShoulder1 = 19, // parent: Spine7 [18]
        LeftArm = 20, // parent: LeftShoulder1 [19]
        LeftForearm = 21, // parent: LeftArm [20]
        LeftHand = 22, // parent: LeftForearm [21]
        LeftHandIndexStart = 23, // parent: LeftHand [22]
        LeftHandIndex1 = 24, // parent: LeftHandIndexStart [23]
        LeftHandIndex2 = 25, // parent: LeftHandIndex1 [24]
        LeftHandIndex3 = 26, // parent: LeftHandIndex2 [25]
        LeftHandIndexEnd = 27, // parent: LeftHandIndex3 [26]
        LeftHandMidStart = 28, // parent: LeftHand [22]
        LeftHandMid1 = 29, // parent: LeftHandMidStart [28]
        LeftHandMid2 = 30, // parent: LeftHandMid1 [29]
        LeftHandMid3 = 31, // parent: LeftHandMid2 [30]
        LeftHandMidEnd = 32, // parent: LeftHandMid3 [31]
        LeftHandPinkyStart = 33, // parent: LeftHand [22]
        LeftHandPinky1 = 34, // parent: LeftHandPinkyStart [33]
        LeftHandPinky2 = 35, // parent: LeftHandPinky1 [34]
        LeftHandPinky3 = 36, // parent: LeftHandPinky2 [35]
        LeftHandPinkyEnd = 37, // parent: LeftHandPinky3 [36]
        LeftHandRingStart = 38, // parent: LeftHand [22]
        LeftHandRing1 = 39, // parent: LeftHandRingStart [38]
        LeftHandRing2 = 40, // parent: LeftHandRing1 [39]
        LeftHandRing3 = 41, // parent: LeftHandRing2 [40]
        LeftHandRingEnd = 42, // parent: LeftHandRing3 [41]
        LeftHandThumbStart = 43, // parent: LeftHand [22]
        LeftHandThumb1 = 44, // parent: LeftHandThumbStart [43]
        LeftHandThumb2 = 45, // parent: LeftHandThumb1 [44]
        LeftHandThumbEnd = 46, // parent: LeftHandThumb2 [45]
        Neck1 = 47, // parent: Spine7 [18]
        Neck2 = 48, // parent: Neck1 [47]
        Neck3 = 49, // parent: Neck2 [48]
        Neck4 = 50, // parent: Neck3 [49]
        Head = 51, // parent: Neck4 [50]
        Jaw = 52, // parent: Head [51]
        Chin = 53, // parent: Jaw [52]
        LeftEye = 54, // parent: Head [51]
        LeftEyeLowerLid = 55, // parent: LeftEye [54]
        LeftEyeUpperLid = 56, // parent: LeftEye [54]
        LeftEyeball = 57, // parent: LeftEye [54]
        Nose = 58, // parent: Head [51]
        RightEye = 59, // parent: Head [51]
        RightEyeLowerLid = 60, // parent: RightEye [59]
        RightEyeUpperLid = 61, // parent: RightEye [59]
        RightEyeball = 62, // parent: RightEye [59]
        RightShoulder1 = 63, // parent: Spine7 [18]
        RightArm = 64, // parent: RightShoulder1 [63]
        RightForearm = 65, // parent: RightArm [64]
        RightHand = 66, // parent: RightForearm [65]
        RightHandIndexStart = 67, // parent: RightHand [66]
        RightHandIndex1 = 68, // parent: RightHandIndexStart [67]
        RightHandIndex2 = 69, // parent: RightHandIndex1 [68]
        RightHandIndex3 = 70, // parent: RightHandIndex2 [69]
        RightHandIndexEnd = 71, // parent: RightHandIndex3 [70]
        RightHandMidStart = 72, // parent: RightHand [66]
        RightHandMid1 = 73, // parent: RightHandMidStart [72]
        RightHandMid2 = 74, // parent: RightHandMid1 [73]
        RightHandMid3 = 75, // parent: RightHandMid2 [74]
        RightHandMidEnd = 76, // parent: RightHandMid3 [75]
        RightHandPinkyStart = 77, // parent: RightHand [66]
        RightHandPinky1 = 78, // parent: RightHandPinkyStart [77]
        RightHandPinky2 = 79, // parent: RightHandPinky1 [78]
        RightHandPinky3 = 80, // parent: RightHandPinky2 [79]
        RightHandPinkyEnd = 81, // parent: RightHandPinky3 [80]
        RightHandRingStart = 82, // parent: RightHand [66]
        RightHandRing1 = 83, // parent: RightHandRingStart [82]
        RightHandRing2 = 84, // parent: RightHandRing1 [83]
        RightHandRing3 = 85, // parent: RightHandRing2 [84]
        RightHandRingEnd = 86, // parent: RightHandRing3 [85]
        RightHandThumbStart = 87, // parent: RightHand [66]
        RightHandThumb1 = 88, // parent: RightHandThumbStart [87]
        RightHandThumb2 = 89, // parent: RightHandThumb1 [88]
        RightHandThumbEnd = 90, // parent: RightHandThumb2 [89]
    }

    public static XRHumanBodyJoint GetXRHumanBodyJoint(ARHumanBody body, HumanBodyBones fvxBone)
    {
        switch (fvxBone)
        {
            case HumanBodyBones.Hips:
                return body.joints[(int)JointIndices.Hips];
            case HumanBodyBones.LeftUpperLeg:
                return body.joints[(int)JointIndices.LeftUpLeg];
            case HumanBodyBones.RightUpperLeg:
                return body.joints[(int)JointIndices.RightUpLeg];
            case HumanBodyBones.LeftLowerLeg:
                return body.joints[(int)JointIndices.LeftLeg];
            case HumanBodyBones.RightLowerLeg:
                return body.joints[(int)JointIndices.RightLeg];
            case HumanBodyBones.LeftFoot:
                return body.joints[(int)JointIndices.LeftFoot];
            case HumanBodyBones.RightFoot:
                return body.joints[(int)JointIndices.RightFoot];
            case HumanBodyBones.Spine:
                return body.joints[(int)JointIndices.Spine1];
            case HumanBodyBones.Chest:
                return body.joints[(int)JointIndices.Spine6];
            case HumanBodyBones.UpperChest:
                return body.joints[(int)JointIndices.Spine7];
            case HumanBodyBones.Neck:
                return body.joints[(int)JointIndices.Neck1];
            case HumanBodyBones.Head:
                return body.joints[(int)JointIndices.Head];
            case HumanBodyBones.LeftShoulder:
                return body.joints[(int)JointIndices.LeftShoulder1];
            case HumanBodyBones.RightShoulder:
                return body.joints[(int)JointIndices.RightShoulder1];
            case HumanBodyBones.LeftUpperArm:
                return body.joints[(int)JointIndices.LeftArm];
            case HumanBodyBones.RightUpperArm:
                return body.joints[(int)JointIndices.RightArm];
            case HumanBodyBones.LeftLowerArm:
                return body.joints[(int)JointIndices.LeftForearm];
            case HumanBodyBones.RightLowerArm:
                return body.joints[(int)JointIndices.RightForearm];
            case HumanBodyBones.LeftHand:
                return body.joints[(int)JointIndices.LeftHand];
            case HumanBodyBones.RightHand:
                return body.joints[(int)JointIndices.RightHand];
            case HumanBodyBones.LeftToes:
                return body.joints[(int)JointIndices.LeftToes];
            case HumanBodyBones.RightToes:
                return body.joints[(int)JointIndices.RightToes];
            case HumanBodyBones.LeftEye:
                return body.joints[(int)JointIndices.LeftEye];
            case HumanBodyBones.RightEye:
                return body.joints[(int)JointIndices.RightEye];
            case HumanBodyBones.Jaw:
                return body.joints[(int)JointIndices.Jaw];
            case HumanBodyBones.LeftThumbProximal:
                return body.joints[(int)JointIndices.LeftHandThumbStart];
            case HumanBodyBones.LeftThumbIntermediate:
                return body.joints[(int)JointIndices.LeftHandThumb1];
            case HumanBodyBones.LeftThumbDistal:
                return body.joints[(int)JointIndices.LeftHandThumb2];
            case HumanBodyBones.LeftIndexProximal:
                return body.joints[(int)JointIndices.LeftHandIndex1];
            case HumanBodyBones.LeftIndexIntermediate:
                return body.joints[(int)JointIndices.LeftHandIndex2];
            case HumanBodyBones.LeftIndexDistal:
                return body.joints[(int)JointIndices.LeftHandIndex3];
            case HumanBodyBones.LeftMiddleProximal:
                return body.joints[(int)JointIndices.LeftHandMid1];
            case HumanBodyBones.LeftMiddleIntermediate:
                return body.joints[(int)JointIndices.LeftHandMid2];
            case HumanBodyBones.LeftMiddleDistal:
                return body.joints[(int)JointIndices.LeftHandMid3];
            case HumanBodyBones.LeftRingProximal:
                return body.joints[(int)JointIndices.LeftHandRing1];
            case HumanBodyBones.LeftRingIntermediate:
                return body.joints[(int)JointIndices.LeftHandRing2];
            case HumanBodyBones.LeftRingDistal:
                return body.joints[(int)JointIndices.LeftHandRing3];
            case HumanBodyBones.LeftLittleProximal:
                return body.joints[(int)JointIndices.LeftHandPinky1];
            case HumanBodyBones.LeftLittleIntermediate:
                return body.joints[(int)JointIndices.LeftHandPinky2];
            case HumanBodyBones.LeftLittleDistal:
                return body.joints[(int)JointIndices.LeftHandPinky3];
            case HumanBodyBones.RightThumbProximal:
                return body.joints[(int)JointIndices.RightHandThumbStart];
            case HumanBodyBones.RightThumbIntermediate:
                return body.joints[(int)JointIndices.RightHandThumb1];
            case HumanBodyBones.RightThumbDistal:
                return body.joints[(int)JointIndices.RightHandThumb2];
            case HumanBodyBones.RightIndexProximal:
                return body.joints[(int)JointIndices.RightHandIndex1];
            case HumanBodyBones.RightIndexIntermediate:
                return body.joints[(int)JointIndices.RightHandIndex2];
            case HumanBodyBones.RightIndexDistal:
                return body.joints[(int)JointIndices.RightHandIndex3];
            case HumanBodyBones.RightMiddleProximal:
                return body.joints[(int)JointIndices.RightHandMid1];
            case HumanBodyBones.RightMiddleIntermediate:
                return body.joints[(int)JointIndices.RightHandMid2];
            case HumanBodyBones.RightMiddleDistal:
                return body.joints[(int)JointIndices.RightHandMid3];
            case HumanBodyBones.RightRingProximal:
                return body.joints[(int)JointIndices.RightHandRing1];
            case HumanBodyBones.RightRingIntermediate:
                return body.joints[(int)JointIndices.RightHandRing2];
            case HumanBodyBones.RightRingDistal:
                return body.joints[(int)JointIndices.RightHandRing3];
            case HumanBodyBones.RightLittleProximal:
                return body.joints[(int)JointIndices.RightHandPinky1];
            case HumanBodyBones.RightLittleIntermediate:
                return body.joints[(int)JointIndices.RightHandPinky2];
            case HumanBodyBones.RightLittleDistal:
                return body.joints[(int)JointIndices.RightHandPinky3];
            default:
                return body.joints[(int)JointIndices.Invalid];
        }
    }
}

