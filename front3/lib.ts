import { ccrSearch } from "@/features/ccrs/search/types/schema";

function normalizeSearch(form: ccrSearch) {
  return {
    fromCcrId: form.fromccrId || null,
    toCcrId: form.toccrId || null,

    fromDateSubmitted: form.fromdatesubmited ?? null,
    toDateSubmitted: form.todatesubmited ?? null,

    teamLeadId:
      form.teamLead === -1 ? null : form.teamLead,

    leadTAId:
      form.leadTA === -1 ? null : form.leadTA,

    sortingOutputId:
      form.sortingOutput === -1 ? null : form.sortingOutput,

    triageOutputId:
      form.triageOutput === -1 ? null : form.triageOutput,

    submittedByFPT:
      form.submittedByFPT === -1 ? null : !!form.submittedByFPT,

    triageDateSet:
      form.triageDateSet === -1 ? null : !!form.triageDateSet,

    triageDate: form.triageDate ?? null,

    sortingCommitteeId:
      form.sortingCommitteeId === -1
        ? null
        : form.sortingCommitteeId,

    triageCommitteeId:
      form.triageCommitteeId === -1
        ? null
        : form.triageCommitteeId,
  };
}

export { normalizeSearch };