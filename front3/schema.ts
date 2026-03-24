import z from "zod";

const ccrSearchSchema = z.object({
 fromccrId: z
  .string()
  .optional()
  .refine(
    (val) => val === undefined || val === "" || !isNaN(Number(val)),
    "Must be a number"
  ),

toccrId: z
  .string()
  .optional()
  .refine(
    (val) => val === undefined || val === "" || !isNaN(Number(val)),
    "Must be a number"
  ),

  fromdatesubmited: z.coerce.date().optional(),
  todatesubmited: z.coerce.date().optional(),
  proponentId: z.number().optional(),
  teamLead: z.number().optional(),
  leadTA: z.number().optional(),
  submittedByFPT: z.number().optional(),
  sortingOutput: z.number().optional(),
  triageOutput: z.number().optional(),
  triageDateSet: z.number().optional(),
  triageDate: z.coerce.date().optional(),
  sortingCommitteeId: z.number().optional(),
  triageCommitteeId: z.number().optional(),
});

type ccrSearch = z.infer<typeof ccrSearchSchema>;
const defaultValues: ccrSearch = {
  fromccrId: "",
  toccrId: "",
  fromdatesubmited: undefined,
  todatesubmited: undefined,
  proponentId: undefined,
  submittedByFPT: -1,
  teamLead: -1,
  leadTA: -1,
  sortingOutput: -1,
  triageOutput: -1,
  triageDateSet: -1,
  triageDate: undefined,
  sortingCommitteeId: -1,
  triageCommitteeId: -1,
};
export { ccrSearchSchema, type ccrSearch, defaultValues };
