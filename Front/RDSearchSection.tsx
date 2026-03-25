"use client";

import { useState, useEffect } from "react";
import SectionControl from "@/features/form/components/controllers/SectionControl";
import { FormErrorSummary } from "@/features/form/components/form-error-summary";
import {
  Box,
  Button,
  FormLabel,
  Grid,
  Stack,
  Tooltip,
  IconButton,
  InputAdornment,
} from "@mui/material";
import { FormProvider, SubmitHandler, useForm, useWatch } from "react-hook-form";
import { TextField } from "@/features/form/components/controllers/text-field";
import { Autocomplete } from "@/features/form/components/controllers/autocomplete";
import { DatePicker } from "@/features/form/components/controllers/date-picker";
import SearchIcon from "@mui/icons-material/Search";
import SearchOffIcon from '@mui/icons-material/SearchOff';
import HelpOutlineIcon from '@mui/icons-material/HelpOutline';
import RDUpdatesDataGrid from "./RDUpdatesDataGrid";
import { fetchAllStatuses, fetchSubStatusesByStatusId, StatusOption, SubStatusOption } from "@/utils/utilReferenceDocuments";

export type RDUpdateSearch = {
  documentNumberFrom?: string;
  documentNumberTo?: string;
  committee?: string;
  agency?: string;
  code?: string;
  codeReference?: string;
  submittedDateFrom?: Date;
  submittedDateTo?: Date;
  having?: string;
  statusValue?: string;
  decision?: string;
  statusCommittee?: string;
  minutesReference?: string;
  additionalCommittee?: string;
  additionalAgency?: string;
  additionalCode?: string;
  additionalCodeReference?: string;
  publicReview?: string;
  includeExclude?: string;
};

export const defaultValues: RDUpdateSearch = {
  documentNumberFrom: "",
  documentNumberTo: "",
  committee: "",
  agency: "",
  code: "",
  codeReference: "",
  submittedDateFrom: undefined,
  submittedDateTo: undefined,
  having: "Having",
  statusValue: "",
  decision: "",
  statusCommittee: "",
  minutesReference: "",
  additionalCommittee: "",
  additionalAgency: "",
  additionalCode: "",
  additionalCodeReference: "",
  publicReview: "",
  includeExclude: "Exclude",
};

const havingOptions = [
  { label: "Having", value: "Having" },
  { label: "Not having", value: "Not having" },
];

// Status IDs that have sub-statuses (decisions)
const STATUS_IDS_WITH_SUBSTATUSES = [3, 6]; // 3 = Reviewed, 6 = Review of Public Review comments

const defaultDecisionOptions = [
  { label: "ANY", value: "" },
  { label: "NO DECISION", value: "NO DECISION" },
];

const committeeOptions = [
  { label: "ANY", value: "" },
];

const additionalCommitteeOptions = [
  { label: "ANY", value: "" },
  { label: "(placeholder)", value: "(placeholder)" },
];

const additionalAgencyOptions = [
  { label: "ANY", value: "" },
  { label: "(placeholder)", value: "(placeholder)" },
];

const additionalCodeOptions = [
  { label: "ANY", value: "" },
  { label: "(placeholder)", value: "(placeholder)" },
];

const publicReviewOptions = [
  { label: "ANY", value: "" },
  { label: "Post", value: "Post" },
  { label: "Conflict", value: "Conflict" },
];

const includeExcludeOptions = [
  { label: "Include updates that are ready for P&M processing, published, or completed", value: "Include" },
  { label: "Exclude updates that are ready for P&M processing, published, or completed", value: "Exclude" },
];

interface RDSearcSectionProps {
  refreshKey: number;
  onRefreshChange: (newKey: number) => void;
}

export default function RDSearchSection({ refreshKey, onRefreshChange }: RDSearcSectionProps) {
  const [expanded, setExpanded] = useState(false);
  const [hasSearched, setHasSearched] = useState(false);
  const [statusOptions, setStatusOptions] = useState<{ label: string; value: string; id?: number }[]>([
    { label: "ANY", value: "" },
  ]);
  const [decisionOptions, setDecisionOptions] = useState(defaultDecisionOptions);

  const form = useForm<RDUpdateSearch>({
    mode: "all",
    defaultValues: defaultValues,
  });

  // Watch the status field for changes
  const selectedStatus = useWatch({
    control: form.control,
    name: "statusValue",
  });

  // Fetch statuses on mount
  useEffect(() => {
    const loadStatuses = async () => {
      try {
        const statuses = await fetchAllStatuses();
        const options = [
          { label: "ANY", value: "", id: undefined },
          ...statuses.map((s) => ({
            label: s.name || "",
            value: s.name || "",
            id: s.id,
          })),
        ];
        setStatusOptions(options);
      } catch (error) {
        console.error("Failed to load statuses:", error);
      }
    };
    loadStatuses();
  }, []);

  // Update decision options when status changes
  useEffect(() => {
    const updateDecisionOptions = async () => {
      // Find the selected status option to get its ID
      const selectedOption = statusOptions.find((opt) => opt.value === selectedStatus);
      const statusId = selectedOption?.id;

      // Check if this status has sub-statuses
      if (statusId && STATUS_IDS_WITH_SUBSTATUSES.includes(statusId)) {
        try {
          const subStatuses = await fetchSubStatusesByStatusId(statusId);
          const options = [
            { label: "ANY", value: "" },
            { label: "NO DECISION", value: "NO DECISION" },
            ...subStatuses.map((s) => ({
              label: s.name || "",
              value: s.name || "",
            })),
          ];
          setDecisionOptions(options);
        } catch (error) {
          console.error("Failed to load sub-statuses:", error);
          setDecisionOptions(defaultDecisionOptions);
        }
      } else {
        // Reset to default options for statuses without sub-statuses
        setDecisionOptions(defaultDecisionOptions);
      }

      // Reset the decision field when status changes
      form.setValue("decision", "");
    };

    updateDecisionOptions();
  }, [selectedStatus, statusOptions, form]);

  const handleSubmit: SubmitHandler<RDUpdateSearch> = async (formData) => {
    console.log("Form Data Submitted: ", formData);
    setHasSearched(true);
    setExpanded(false);
    onRefreshChange(refreshKey + 1);
  };

  const handleReset = () => {
    form.reset();
    setHasSearched(false);
    setExpanded(true);
  };

  return (
    <Grid container spacing={2} component="form" onSubmit={form.handleSubmit(handleSubmit)}>
      <Grid size={{ xs: 12, md: 12 }}>
        <FormProvider {...form}>
          <FormErrorSummary />
          <Grid size={{ xs: 12 }}>
            <SectionControl
              icon={<SearchIcon />}
              title="Search Criteria"
              columnSize={3}
              expanded={expanded}
              onExpandedChange={setExpanded}
            >
              {/* Update ID */}
              <Grid size={{ xs: 12, md: 6 }}>
                <Box
                  sx={{
                    border: "1px solid",
                    borderColor: "divider",
                    borderRadius: 1,
                    p: 2,
                    width: "100%",
                  }}
                >
                  <Stack spacing={2}>
                    <FormLabel component="legend" sx={{ fontWeight: "bold" }}>
                      Update ID
                    </FormLabel>
                    <Stack spacing={2} direction="row">
                      <TextField<RDUpdateSearch> name="documentNumberFrom" label={"From"} fullWidth />
                      <TextField<RDUpdateSearch> name="documentNumberTo" label={"To"} fullWidth />
                    </Stack>
                  </Stack>
                </Box>
              </Grid>

              {/* Date Submitted */}
              <Grid size={{ xs: 12, md: 6 }}>
                <Box
                  sx={{
                    border: "1px solid",
                    borderColor: "divider",
                    borderRadius: 1,
                    p: 2,
                    width: "100%",
                  }}
                >
                  <Stack spacing={2}>
                    <FormLabel component="legend" sx={{ fontWeight: "bold" }}>
                      Date Submitted
                    </FormLabel>
                    <Stack spacing={2} direction="row">
                      <DatePicker<RDUpdateSearch> name="submittedDateFrom" label="From" />
                      <DatePicker<RDUpdateSearch> name="submittedDateTo" label="To" />
                    </Stack>
                  </Stack>
                </Box>
              </Grid>

              {/* Status Box */}
              <Grid size={{ xs: 12, md: 12 }}>
                <Box
                  sx={{
                    border: "1px solid",
                    borderColor: "divider",
                    borderRadius: 1,
                    p: 2,
                    width: "100%",
                  }}
                >
                  <Stack spacing={2}>
                    <FormLabel component="legend" sx={{ fontWeight: "bold" }}>
                      Status
                    </FormLabel>
                    
                    {/* First line: Having/Not having, Status, Decision */}
                    <Grid container spacing={2}>
                      <Grid size={{ xs: 12, md: 2.935 }}>
                        <Autocomplete<RDUpdateSearch>
                          name="having"
                          textFieldProps={{
                            label: "",
                          }}
                          options={havingOptions}
                        />
                      </Grid>
                      <Grid size={{ xs: 12, md: 6.13 }}>
                        <Autocomplete<RDUpdateSearch>
                          name="statusValue"
                          textFieldProps={{
                            label: "Status",
                          }}
                          options={statusOptions}
                        />
                      </Grid>
                      <Grid size={{ xs: 12, md: 2.93 }}>
                        <Autocomplete<RDUpdateSearch>
                          name="decision"
                          textFieldProps={{
                            label: "Decision",
                          }}
                          options={decisionOptions}
                        />
                      </Grid>
                    </Grid>

                    {/* Second line: Committee, Minutes Reference */}
                    <Grid container spacing={2}>
                      <Grid size={{ xs: 12, md: 6 }}>
                        <Autocomplete<RDUpdateSearch>
                          name="statusCommittee"
                          textFieldProps={{
                            label: "Committee",
                          }}
                          options={committeeOptions}
                        />
                      </Grid>
                      <Grid size={{ xs: 12, md: 6 }}>
                        <TextField<RDUpdateSearch> name="minutesReference" label="Minutes Reference" fullWidth />
                      </Grid>
                    </Grid>
                  </Stack>
                </Box>
              </Grid>

              {/* Additional Box */}
              <Grid size={{ xs: 12, md: 12 }}>
                <Box
                  sx={{
                    border: "1px solid",
                    borderColor: "divider",
                    borderRadius: 1,
                    p: 2,
                    width: "100%",
                  }}
                >
                  <Stack spacing={2}>
                    <FormLabel component="legend" sx={{ fontWeight: "bold" }}>
                      Additional
                    </FormLabel>
                    
                    {/* First line: Committee, Agency, Code */}
                    <Grid container spacing={2}>
                      <Grid size={{ xs: 12, md: 2.935 }}>
                        <Autocomplete<RDUpdateSearch>
                          name="additionalCommittee"
                          textFieldProps={{
                            label: "Committee",
                          }}
                          options={additionalCommitteeOptions}
                        />
                      </Grid>
                      <Grid size={{ xs: 12, md: 3.06 }}>
                        <Autocomplete<RDUpdateSearch>
                          name="additionalAgency"
                          textFieldProps={{
                            label: "Agency",
                          }}
                          options={additionalAgencyOptions}
                        />
                      </Grid>
                      <Grid size={{ xs: 12, md: 3.07 }}>
                        <Autocomplete<RDUpdateSearch>
                          name="additionalCode"
                          textFieldProps={{
                            label: "Code",
                          }}
                          options={additionalCodeOptions}
                        />
                      </Grid>
                      <Grid size={{ xs: 12, md: 2.935 }}>
                        <TextField<RDUpdateSearch> 
                          name="additionalCodeReference" 
                          label="Code Reference" 
                          fullWidth 
                          slotProps={{
                            input: {
                              endAdornment: (
                                <InputAdornment position="end">
                                  <Tooltip title="Enter a code reference (e.g. 3.5.2.1.(2))" arrow>
                                    <IconButton size="small" edge="end">
                                      <HelpOutlineIcon fontSize="small" />
                                    </IconButton>
                                  </Tooltip>
                                </InputAdornment>
                              ),
                            },
                          }}
                        />
                      </Grid>
                    </Grid>

                    {/* Second line: Public Review, Include/Exclude */}
                    <Grid container spacing={2}>
                      <Grid size={{ xs: 12, md: 2.935 }}>
                        <Autocomplete<RDUpdateSearch>
                          name="publicReview"
                          textFieldProps={{
                            label: "Public Review",
                          }}
                          options={publicReviewOptions}
                        />
                      </Grid>
                      <Grid size={{ xs: 12, md: 9.062 }}>
                        <Autocomplete<RDUpdateSearch>
                          name="includeExclude"
                          textFieldProps={{
                            label: "",
                          }}
                          options={includeExcludeOptions}
                        />
                      </Grid>
                    </Grid>
                  </Stack>
                </Box>
              </Grid>

              {/* Search and Reset Buttons */}
              <Grid size={{ xs: 12, md: 12 }} />
              <Grid size={{ xs: 6, md: 2 }}>
                <Button
                  startIcon={<SearchIcon />}
                  variant="contained"
                  color="primary"
                  type="submit"
                  fullWidth
                  onClick={() => setExpanded(false)}
                >
                  Search
                </Button>
              </Grid>
              <Grid size={{ xs: 6, md: 2 }}>
                <Button
                  startIcon={<SearchOffIcon />}
                  variant="outlined"
                  color="primary"
                  type="button"
                  fullWidth
                  onClick={handleReset}
                >
                  Reset
                </Button>
              </Grid>
            </SectionControl>
          </Grid>
        </FormProvider>
      </Grid>

      {hasSearched && (
        <Grid size={{ xs: 12, md: 12 }}>
          <RDUpdatesDataGrid 
            editButtonCallback={(row) => { console.log("Edit row:", row); }}
            refreshKey={refreshKey}
          />
        </Grid>
      )}
    </Grid>
  );
}
