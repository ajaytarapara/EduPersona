import {
  Box,
  Typography,
  styled,
  Stepper,
  Step,
  StepLabel,
  InputLabel,
  TextField,
  Stack,
} from "@mui/material";
import {
  FormProvider,
  useForm,
  Controller,
  type SubmitHandler,
} from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { DatePicker } from "@mui/x-date-pickers";
import {
  CustomButton,
  InputField,
  MultiSelectChipField,
  SelectField,
} from "../components";
import {
  AppRoutes,
  profileSchema,
  type ICompleteProfilePayload,
  type IProfileFormValues,
  type ISelectOption,
} from "../utils";
import { getDesignations, getProfessions, getSkills } from "../api/masterData";
import { useEffect, useState } from "react";
import { completeProfile } from "../api";
import { useAppDispatch } from "../store/hook";
import { setProfileComplete } from "../store/features";
import { useNavigate } from "react-router-dom";
const steps = ["Personal Info", "Career Details", "Skills"];

const ProfileCompletionPage = () => {
  const [activeStep, setActiveStep] = useState(0);
  const [professions, setProfessions] = useState<ISelectOption[]>([]);
  const [designations, setDesignations] = useState<ISelectOption[]>([]);
  const [skills, setSkills] = useState<ISelectOption[]>([]);
  const [loadingDesignation, setLoadingDesignation] = useState(false);
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const methods = useForm({
    mode: "onTouched",
    resolver: yupResolver(profileSchema),
    defaultValues: {
      address: "",
      phoneNo: "",
      professionId: 0,
      currentDesignationId: 0,
      targetDesignationId: 0,
      skillIds: [],
    },
  });

  const { watch, trigger, setValue } = methods;
  const professionId = watch("professionId");
  const currentDesignationId = watch("currentDesignationId");

  /* ---------- Load Initial Data ---------- */
  useEffect(() => {
    const load = async () => {
      const prof = await getProfessions();
      const skill = await getSkills();
      setProfessions(prof.data);
      setSkills(skill.data);
    };
    load();
  }, []);

  /* ---------- Load Designations ---------- */
  useEffect(() => {
    if (!professionId) return;

    setLoadingDesignation(true);
    getDesignations(professionId).then((res) => {
      setDesignations(res.data);
      setLoadingDesignation(false);
      setValue("currentDesignationId", 0);
      setValue("targetDesignationId", 0);
    });
  }, [professionId]);

  /* ---------- Step Navigation ---------- */

  const handleNext = async () => {
    if (activeStep === 0) {
      const valid = await trigger(["birthdate", "address", "phoneNo"]);
      if (!valid) return;
    }

    if (activeStep === 1) {
      const valid = await trigger([
        "professionId",
        "currentDesignationId",
        "targetDesignationId",
      ]);
      if (!valid) return;
    }

    if (activeStep === 2) {
      const valid = await trigger(["skillIds"]);
      if (!valid) return;
    }

    setActiveStep((prev) => prev + 1);
  };

  const handleBack = () => {
    setActiveStep((prev) => prev - 1);
  };

  const onSubmit: SubmitHandler<IProfileFormValues> = async (data) => {
    const payload: ICompleteProfilePayload = {
      ...data,
      birthdate: data.birthdate ? data.birthdate.toISOString() : null,
      address: data.address ?? undefined,
    };

    const response = await completeProfile(payload);
    if (response.success) {
      dispatch(setProfileComplete());
      navigate(AppRoutes.Profile);
    }
  };

  return (
    <StyledWrapper>
      <StyledCard>
        <Typography variant="h4" fontWeight={700} textAlign="center">
          Complete Your Profile
        </Typography>

        <Stepper activeStep={activeStep} sx={{ mt: 3, mb: 4 }}>
          {steps.map((label) => (
            <Step key={label}>
              <StepLabel>{label}</StepLabel>
            </Step>
          ))}
        </Stepper>

        <FormProvider {...methods}>
          <StyledForm onSubmit={methods.handleSubmit(onSubmit)}>
            {/* ---------------- STEP 1 ---------------- */}
            {activeStep === 0 && (
              <>
                <StyledInputLabel>Birthdate</StyledInputLabel>

                <Controller
                  name="birthdate"
                  control={methods.control}
                  render={({ field, fieldState }) => (
                    <DatePicker
                      value={field.value}
                      onChange={(val) => field.onChange(val)}
                      format="DD/MM/YYYY"
                      disableFuture
                      slotProps={{
                        textField: {
                          fullWidth: true,
                          placeholder: "DD/MM/YYYY",
                          error: !!fieldState.error,
                          helperText: fieldState.error?.message,
                          sx: {
                            mt: 1,
                            mb: "24px",

                            "& .MuiPickersOutlinedInput-root": {
                              borderRadius: "12px",
                              height: "46px",
                            },

                            "& .MuiPickersInputBase-input": {
                              padding: "12px 14px",
                            },

                            "& .MuiInputAdornment-positionEnd": {
                              display: "none",
                            },
                          },
                        },
                      }}
                    />
                  )}
                />

                <InputField
                  name="address"
                  label="Address"
                  fullWidth
                  style={{ marginBottom: "24px" }}
                  placeholder="e.g., 221B Baker Street, London"
                />

                <InputField
                  name="phoneNo"
                  required
                  label="Phone Number"
                  fullWidth
                  placeholder="e.g., 9876543210"
                />

                <CustomButton
                  variant="contained"
                  fullWidth
                  sx={{ mt: 4, height: 48 }}
                  onClick={handleNext}
                >
                  Next
                </CustomButton>
              </>
            )}

            {/* ---------------- STEP 2 ---------------- */}
            {activeStep === 1 && (
              <>
                <Stack spacing={2}>
                  <SelectField
                    name="professionId"
                    label="Profession"
                    required
                    options={professions}
                  />

                  <SelectField
                    name="currentDesignationId"
                    label="Current Designation"
                    required
                    disabled={!professionId || loadingDesignation}
                    options={designations}
                  />

                  <SelectField
                    name="targetDesignationId"
                    label="Target Designation"
                    required
                    disabled={!currentDesignationId}
                    options={designations}
                  />

                  {/* <MultiSelectChipField
                    name="skillIds"
                    label="Skills"
                    options={skills}
                  /> */}
                  {/* ---------------- Skills Section ---------------- */}
                </Stack>
                <Box
                  display="flex"
                  gap={2}
                  mt={4}
                  flexDirection={{ xs: "column", sm: "row" }}
                >
                  <CustomButton
                    variant="outlined"
                    fullWidth
                    onClick={handleBack}
                  >
                    Back
                  </CustomButton>

                  <CustomButton
                    variant="contained"
                    fullWidth
                    onClick={handleNext}
                  >
                    Next
                  </CustomButton>
                </Box>
              </>
            )}

            {activeStep === 2 && (
              <>
                <Stack spacing={2}>
                  <MultiSelectChipField
                    name="skillIds"
                    label="Skills"
                    required
                    options={skills}
                    maxVisibleChips={3}
                  />
                </Stack>

                <Box
                  display="flex"
                  gap={2}
                  mt={4}
                  flexDirection={{ xs: "column", sm: "row" }}
                >
                  <CustomButton
                    variant="outlined"
                    fullWidth
                    onClick={handleBack}
                  >
                    Back
                  </CustomButton>

                  <CustomButton
                    type="submit"
                    variant="contained"
                    fullWidth
                    sx={{ height: 48 }}
                  >
                    Save Profile
                  </CustomButton>
                </Box>
              </>
            )}
          </StyledForm>
        </FormProvider>
      </StyledCard>
    </StyledWrapper>
  );
};

export default ProfileCompletionPage;

/* ---------------- STYLES ---------------- */

const StyledWrapper = styled(Box)(({ theme }) => ({
  minHeight: "100vh",
  display: "flex",
  alignItems: "center",
  justifyContent: "center",
  padding: theme.spacing(3),
  background: theme.palette.background.default,
}));

const StyledCard = styled(Box)(({ theme }) => ({
  width: "100%",
  maxWidth: 600,
  background: theme.palette.background.paper,
  padding: theme.spacing(4),
  borderRadius: 16,
  boxShadow: theme.shadows[4],
}));

const StyledForm = styled("form")({
  marginTop: "10px",
});

const StyledInputLabel = styled(InputLabel)({
  fontSize: 14,
  marginBottom: 8,
});

const StyledDateField = styled(TextField)({
  "& .MuiOutlinedInput-root": {
    borderRadius: 12,
    height: "46px",
  },
});
