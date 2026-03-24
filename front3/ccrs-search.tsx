import SectionControl from "@/features/form/components/controllers/SectionControl";
import { FormErrorSummary } from "@/features/form/components/form-error-summary";
import { zodResolver } from "@hookform/resolvers/zod";
import { Box, Button, FormLabel, Grid, Stack } from "@mui/material";
import { Form, FormProvider, SubmitHandler, useForm } from "react-hook-form";
import {
  defaultValues,
  ccrSearchSchema as Schema,
  ccrSearch,
} from "../types/schema";
import { TextField } from "@/features/form/components/controllers/text-field";
import { Autocomplete } from "@/features/form/components/controllers/autocomplete";
import { DatePicker } from "@/features/form/components/controllers/date-picker";
import {
  useCodeSortingOutputs,
  useCommittees,
  useLeadTAs,
  useTeamLeads,
} from "@/features/ccr-detail/hooks/useQueries";
import { useEffect, useMemo, useState } from "react";
import CCRDataGridView from "./CCRDataGridView";
import { useSearchCCRs } from "../hooks/useQueries";
import SearchIcon from "@mui/icons-material/Search";
import SearchOffIcon from "@mui/icons-material/SearchOff";

export default function CCRSSearch({ locale }: { locale: string }) {
  return (
    <div>
      <Provider locale={locale} />
    </div>
  );
}

type ProviderProps = { locale: string };

const Provider = ({ locale }: ProviderProps) => {
  const [expanded, setExpanded] = useState(false);

  const form = useForm<ccrSearch>({
    mode: "all",
    defaultValues: defaultValues,
    resolver: zodResolver(Schema),
  });
  const [hasSearched, setHasSearched] = useState(true);

  const [paginationModel, setPaginationModel] = useState({
    page: 0,
    pageSize: 10,
  });
  const [searchFilters, setSearchFilters] = useState<ccrSearch>(defaultValues);
  const [rowCountState, setRowCountState] = useState(0);

  const { data, isLoading } = useSearchCCRs(
    searchFilters,
    paginationModel.page,
    paginationModel.pageSize,
    hasSearched,
  );

  useEffect(() => {
    if (data?.totalCount !== undefined) {
      setRowCountState(data.totalCount);
    }
  }, [data?.totalCount]);

  const handleSubmit: SubmitHandler<ccrSearch> = async (formData) => {
    try {
      console.log("Form Data Submitted: ", formData);
      setSearchFilters(formData);
      setHasSearched(true);
      setPaginationModel({ page: 0, pageSize: paginationModel.pageSize });
      setExpanded(false);
    } catch (e) {}
  };
  return (
    <Grid
      container
      spacing={2}
      component="form"
      onSubmit={form.handleSubmit(handleSubmit)}
    >
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
              <Page />
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
                  onClick={() => {
                    form.reset();
                    setHasSearched(false);
                    setExpanded(true);
                  }}
                >
                  Reset
                </Button>
              </Grid>
            </SectionControl>
          </Grid>
        </FormProvider>
      </Grid>

      <Grid size={{ xs: 12, md: 12 }}>
        {hasSearched && (
          <CCRDataGridView
            locale={locale}
            rows={data?.items}
            loading={isLoading}
            rowCount={rowCountState}
            paginationModel={paginationModel}
            onPaginationModelChange={setPaginationModel}
          />
        )}
      </Grid>
    </Grid>
  );
};

interface PageProps {}

const Page = ({}: PageProps) => {
  const teamLeadsQuery = useTeamLeads();
  const leadTAsQuery = useLeadTAs();
  const committeesQuery = useCommittees();
  const codeSortingOutputsQuery = useCodeSortingOutputs();

  const teamLeadOptions = useMemo(
    () => [
      { label: "ANY", value: -1 },
      { label: "No Team Lead", value: 0 },
      ...(teamLeadsQuery.data?.map((item) => ({
        label: `${item.firstName} ${item.lastName}`,
        value: item.resourceId,
      })) ?? []),
    ],
    [teamLeadsQuery.data],
  );

  const leadTAOptions = useMemo(
    () => [
      { label: "ANY", value: -1 },
      { label: "No Lead TA", value: 0 },
      ...(leadTAsQuery.data?.map((item) => ({
        label: `${item.firstName} ${item.lastName}`,
        value: item.resourceId,
      })) ?? []),
    ],
    [leadTAsQuery.data],
  );

  const codeSortingOutputs = useMemo(
    () => [
      { label: "ANY", value: -1 },
      { label: "No Sorting Output", value: 0 },
      ...(codeSortingOutputsQuery.data?.map((item) => ({
        label: `${item.sortingOutputTitle_En}`,
        value: item.sortingOutputId,
      })) ?? []),
    ],
    [codeSortingOutputsQuery.data],
  );

  const committeesOptions = useMemo(
    () => [
      { label: "ANY", value: -1 },
      { label: "No Sorting Output", value: 0 },
      ...(committeesQuery.data?.map((item) => ({
        label: `${item.committeeName}`,
        value: item.committeeId,
      })) ?? []),
    ],
    [committeesQuery.data],
  );

  return (
    <>
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
              CCR ID
            </FormLabel>
            <Stack spacing={2} direction="row">
              <TextField<ccrSearch> name="fromccrId" label={"From"} fullWidth />
              <TextField<ccrSearch> name="toccrId" label={"To"} fullWidth />
            </Stack>
          </Stack>
        </Box>
      </Grid>
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
              Submission Date
            </FormLabel>

            <Stack spacing={2} direction="row">
              <DatePicker<ccrSearch> name="fromdatesubmited" label="From" />
              <DatePicker<ccrSearch> name="todatesubmited" label="To" />
            </Stack>
          </Stack>
        </Box>
      </Grid>
      {/* <Grid size={{ xs: 12, md: 6 }}>
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
              Proponent
            </FormLabel>
            <Stack spacing={2} direction="row">
              <TextField<ccrSearch> name="proponentId" />
              <Button variant="outlined" color="info" sx={{width:"200px"}}>
                Select
              </Button>
            </Stack>
          </Stack>
        </Box>
      </Grid> */}

      <Grid size={{ xs: 12, md: 6, lg: 3 }}>
        <Autocomplete<ccrSearch>
          name="submittedByFPT"
          textFieldProps={{
            label: "Submitted By FPT",
          }}
          options={[
            { label: "ANY", value: -1 },
            { label: "YES", value: 1 },
            { label: "NO", value: 0 },
          ]}
        />
      </Grid>

      <Grid size={{ xs: 12, md: 6, lg: 3 }}>
        <Autocomplete<ccrSearch>
          name="teamLead"
          textFieldProps={{
            label: "Team Lead",
          }}
          options={teamLeadOptions}
        />
      </Grid>
      <Grid size={{ xs: 12, md: 6, lg: 3 }}>
        <Autocomplete<ccrSearch>
          name="leadTA"
          textFieldProps={{
            label: "Lead TA",
          }}
          options={leadTAOptions}
        />
      </Grid>

      <Grid size={{ xs: 12, md: 6, lg: 3 }}>
        <Autocomplete<ccrSearch>
          name="sortingOutput"
          textFieldProps={{
            label: "Sorting Output",
          }}
          options={codeSortingOutputs}
        />
      </Grid>
      <Grid size={{ xs: 12, md: 6, lg: 3 }}>
        <Autocomplete<ccrSearch>
          name="triageOutput"
          textFieldProps={{
            label: "Triage Decision",
          }}
          options={codeSortingOutputs}
        />
      </Grid>
      <Grid size={{ xs: 12, md: 6, lg: 3 }}>
        <Autocomplete<ccrSearch>
          name="triageDateSet"
          textFieldProps={{
            label: "Triage Date Set",
          }}
          options={[
            { label: "ANY", value: -1 },
            { label: "YES", value: 1 },
            { label: "NO", value: 0 },
          ]}
        />
      </Grid>
      <Grid size={{ xs: 12, md: 6, lg: 3 }}>
        <DatePicker<ccrSearch> name="triageDate" label="Triage Date" />
      </Grid>

      <Grid size={{ xs: 12, md: 6, lg: 3 }}>
        <Autocomplete<ccrSearch>
          name="sortingCommitteeId"
          textFieldProps={{
            label: "Committee Concerned (Sorting)",
          }}
          options={committeesOptions}
        />
      </Grid>

      <Grid size={{ xs: 12, md: 6, lg: 3 }}>
        <Autocomplete<ccrSearch>
          name="triageCommitteeId"
          textFieldProps={{
            label: "Committee Concerned (Triage)",
          }}
          options={committeesOptions}
        />
      </Grid>
    </>
  );
};
