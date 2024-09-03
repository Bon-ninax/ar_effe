    using UnityEngine;
    using UnityEngine.XR.ARFoundation;
    using UnityEngine.XR.ARKit;
    using System.Collections.Generic;
    using UnityEngine.XR.ARSubsystems;

    public class HumanBodyTracker : MonoBehaviour
    {
        private const int V = 0;
        [SerializeField] GameObject fvxPrehub;

        [SerializeField] Animator animator;

        [SerializeField]
        [Tooltip("The ARHumanBodyManager which will produce body tracking events.")]
        ARHumanBodyManager humanBodyManager;
        private const float jointScaleModifier = .9f;
        // 全身回転補正を指定（必要に応じて調整）
        public Vector3 rotationOffset = new Vector3(0, 0, 0);
        // パーツ回転補正を指定（必要に応じて調整）
        public Vector3 partRotationOffset = new Vector3(-90f, 180f, 0);

        public Vector3 positionOffset = new Vector3(0, 0.9f, -0.5f);

        void Start()
            {
                if (animator == null)
                {
                    animator = GetComponent<Animator>();
                }


            if (humanBodyManager == null)
                {
                    Debug.LogError("ARHumanBodyManager is not found. Please ensure ARKit is correctly set up.");
                }
        }

        private void OnEnable()
            {
                humanBodyManager.humanBodiesChanged += OnFaceChanged;
            humanBodyManager.pose3DScaleEstimationRequested = true;


        }

        private void OnDisable()
        {
            humanBodyManager.humanBodiesChanged -= OnFaceChanged;
            humanBodyManager.pose3DScaleEstimationRequested = true;

        }

    void OnFaceChanged(ARHumanBodiesChangedEventArgs eventArgs)
        {
            if (animator == null)
            {
                animator = GetComponent<Animator>();
            }

            foreach (ARHumanBody humanBody in eventArgs.added)
            {
            Debug.Log("アド!!!!!!!!!!!!!!!");
            fvxPrehub.transform.rotation = humanBody.transform.rotation;

            }

            // トラッキングされたボディが存在するかを確認
                foreach (var humanBody in eventArgs.updated)
                {
            Debug.Log("アップデート!!!!!!!!!!!!!!!!!");
                    var bodyT = humanBody.transform;
                    var bodyR = bodyT.localRotation;
                    var fvxPrehubT = fvxPrehub.transform;

                fvxPrehubT.transform.position = bodyT.position;
                fvxPrehubT.transform.localScale = (new Vector3(humanBody.estimatedHeightScaleFactor, humanBody.estimatedHeightScaleFactor, humanBody.estimatedHeightScaleFactor));
            //Quaternion rotationOffsetQuaternion = Quaternion.Euler(rotationOffset);
            fvxPrehubT.transform.rotation = bodyT.rotation;// * rotationOffsetQuaternion;

            UpdateBone(HumanBodyBones.Hips, humanBody, true, false);
            //UpdateBone(HumanBodyBones.Head, humanBody);
            //UpdateBone(HumanBodyBones.Neck, humanBody);

            //UpdateBone(HumanBodyBones.Chest, humanBody);
            //UpdateBone(HumanBodyBones.UpperChest, humanBody);
            UpdateBone(HumanBodyBones.LeftShoulder, humanBody);

            //UpdateBone(HumanBodyBones.LeftUpperArm, humanBody);
            //UpdateBone(HumanBodyBones.LeftLowerArm, humanBody);
            //UpdateBone(HumanBodyBones.LeftHand, humanBody);
            //UpdateBone(HumanBodyBones.LeftToes, humanBody);
            //UpdateBone(HumanBodyBones.LeftFoot, humanBody);
            //UpdateBone(HumanBodyBones.RightUpperLeg, humanBody);
            //UpdateBone(HumanBodyBones.RightLowerLeg, humanBody);

            //UpdateBone(HumanBodyBones.RightUpperArm, humanBody);
            //UpdateBone(HumanBodyBones.RightLowerArm, humanBody);
            //UpdateBone(HumanBodyBones.RightHand, humanBody);
            //UpdateBone(HumanBodyBones.RightToes, humanBody);
            //UpdateBone(HumanBodyBones.RightFoot, humanBody, false, false);
            //UpdateBone(HumanBodyBones.RightUpperLeg, humanBody);
            //UpdateBone(HumanBodyBones.RightLowerLeg, humanBody);
            //ApplyJointTransforms(humanBody);
        }

    }

    
        //void ApplyJointTransforms(ARHumanBody iosHumanBody, bool applyPositionOffset = false, float scaleCorrection = 0.5f)
        //{
        //    foreach (HumanBodyBones fbxBone in System.Enum.GetValues(typeof(HumanBodyBones)))
        //    {
        //        var joint = HumanoidUtils.GetXRHumanBodyJoint(iosHumanBody, fbxBone);

        //        if (joint.tracked)
        //        {
        //            Transform boneTransform = animator.GetBoneTransform(fbxBone);
        //        if (boneTransform != null)
        //        {
        //            if (fbxBone == HumanBodyBones.Hips)
        //            {

        //                // 回転補正を適用
        //                Quaternion rotationOffsetQuaternion = Quaternion.Euler(partRotationOffset);
        //                //boneTransform.localRotation = joint.anchorPose.rotation * rotationOffsetQuaternion;
        //                //fvxPrehub.transform.parent = boneTransform;

        //            } else
        //            {
        //                Quaternion correctedRotation = new Quaternion(joint.anchorPose.rotation.x, joint.anchorPose.rotation.y * -180f, joint.anchorPose.rotation.z, joint.anchorPose.rotation.w);

        //                boneTransform.localRotation = correctedRotation;
        //            }

        //            //boneTransform.localPosition = joint.localPose.position;
        //            //boneTransform.localRotation = joint.localPose.rotation;

        //            // スケールの補正を適用
        //            //boneTransform.localScale = joint.anchorScale * jointScaleModifier;
        //            boneTransform.localPosition = joint.anchorPose.position;// * iosHumanBody.estimatedHeightScaleFactor;
        //            //boneTransform.localScale = joint.anchorScale;// * jointScaleModifier;
                    
        //            //boneTransform.localPosition = correctedPosition;
                    
        //        }
        //        }
        //    }
        //}

    void UpdateBone(HumanBodyBones bone, ARHumanBody humanBody, bool applyPositionOffset = false, bool onlyPosition = false)
    {
        int jointIndex = GetARKitJointIndex(bone);

        var joint = humanBody.joints[jointIndex];//HumanoidUtils.GetXRHumanBodyJoint(humanBody, bone);


        if (joint.tracked)
        {
            Transform boneTransform = animator.GetBoneTransform(bone);
            if (boneTransform != null)
            {

                // 位置とスケールの設定
                Vector3 adjustedPosition = joint.anchorPose.position;
                if (onlyPosition)
                {
                    Quaternion rotationOffsetQuaternion = Quaternion.Euler(new Vector3(-90f, 180f, 0));
                    boneTransform.localRotation = joint.anchorPose.rotation * rotationOffsetQuaternion;
                    // ルートボーンの位置補正
                    //adjustedPosition += positionOffset;
                }

                if (applyPositionOffset)
                {

                    // 回転補正を適用
                    Quaternion rotationOffsetQuaternion = Quaternion.Euler(new Vector3(-90f, 180f, 0));
                    boneTransform.localRotation = joint.anchorPose.rotation * rotationOffsetQuaternion;
                }
                else
                {
                    Quaternion correctedRotation = new Quaternion(joint.anchorPose.rotation.x, joint.anchorPose.rotation.y, joint.anchorPose.rotation.z*-90f, joint.anchorPose.rotation.w);

                    boneTransform.localRotation = correctedRotation;
                }
                boneTransform.localPosition = adjustedPosition * humanBody.estimatedHeightScaleFactor;
                //boneTransform.localScale = joint.anchorScale * jointScaleModifier;
            }
        }
    }


    int GetARKitJointIndex(HumanBodyBones bone)
    {
        switch (bone)
        {
            case HumanBodyBones.Hips: return 1;
            case HumanBodyBones.Head: return 51;
            case HumanBodyBones.Neck: return 11;
            case HumanBodyBones.LeftShoulder: return 19;
            case HumanBodyBones.RightShoulder: return 63;
            case HumanBodyBones.LeftUpperArm: return 20;
            case HumanBodyBones.RightUpperArm: return 64;
            case HumanBodyBones.LeftLowerArm: return 21; // Elbow and lower arm might share same index
            case HumanBodyBones.RightLowerArm: return 65; // Same as above
            case HumanBodyBones.LeftHand: return 22;
            case HumanBodyBones.RightHand: return 66;
            case HumanBodyBones.LeftUpperLeg: return 1;
            case HumanBodyBones.RightUpperLeg: return 7;
            case HumanBodyBones.LeftLowerLeg: return 3;
            case HumanBodyBones.RightLowerLeg: return 8;
            case HumanBodyBones.LeftFoot: return 4;
            case HumanBodyBones.RightFoot: return 9;
            default: return -1; // 対応するインデックスが無い場合
        }
    }
}