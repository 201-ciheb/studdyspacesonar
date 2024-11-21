using Newtonsoft.Json;

namespace PHIASPACE.INTERFACE.Models;
public class Attachment
{
    public string download_url { get; set; }
    public string download_large_url { get; set; }
    public string download_medium_url { get; set; }
    public string download_small_url { get; set; }
    public string mimetype { get; set; }
    public string filename { get; set; }
    public int instance { get; set; }
    public int xform { get; set; }
    public int id { get; set; }
}
public class SatLabApiResponse
{
    public int _id { get; set; }

    [JsonProperty("formhub/uuid")]
    public string formhubuuid { get; set; }
    public string EntryDate { get; set; }
    public DateTime start { get; set; }
    public DateTime end { get; set; }
    [JsonProperty("Page1/interviewer")]
    public string Page1interviewer { get; set; }
    [JsonProperty("Page1/team")]
    public string Page1team { get; set; }
    [JsonProperty("Page1/assessment_date")]
    public string Page1assessment_date { get; set; }
    [JsonProperty("Page1/county")]
    public string Page1county { get; set; }
    [JsonProperty("Page1/subcounty")]
    public string Page1subcounty { get; set; }
    [JsonProperty("Page1/facility")]
    public string Page1facility { get; set; }
    [JsonProperty("Page1/kmfl")]
    public string Page1kmfl { get; set; }
    [JsonProperty("Page1/keph")]
    public string Page1keph { get; set; }
    [JsonProperty("Page1/ward")]
    public string Page1ward { get; set; }
    [JsonProperty("Page1/ownership")]
    public string Page1ownership { get; set; }
    [JsonProperty("Page1/geopoint_widget")]
    public string Page1geopoint_widget { get; set; }
    [JsonProperty("Page1/postal-address")]
    public string Page1postaladdress { get; set; }
    [JsonProperty("Page1/physical_lands")]
    public string Page1physical_lands { get; set; }
    [JsonProperty("Page1/facility_email")]
    public string Page1facility_email { get; set; }
    [JsonProperty("Page1/incharge")]
    public string Page1incharge { get; set; }
    [JsonProperty("Page1/phone")]
    public string Page1phone { get; set; }
    [JsonProperty("Page1/email")]
    public string Page1email { get; set; }
    [JsonProperty("Page1/lab_manager")]
    public string Page1lab_manager { get; set; }
    [JsonProperty("Page1/lab_phone")]
    public string Page1lab_phone { get; set; }
    [JsonProperty("Page1/lab_email")]
    public string Page1lab_email { get; set; }
    [JsonProperty("Page1/county_lab_manager")]
    public string Page1county_lab_manager { get; set; }
    [JsonProperty("Page1/county_lab_phone")]
    public string Page1county_lab_phone { get; set; }
    [JsonProperty("Page1/county_email")]
    public string Page1county_email { get; set; }
    [JsonProperty("Page1b/lab_level")]
    public string Page1blab_level { get; set; }
    [JsonProperty("Page1b/lab_aff")]
    public string Page1blab_aff { get; set; }
    [JsonProperty("Page1c/accessibility")]
    public string Page1caccessibility { get; set; }
    [JsonProperty("Page1c/road")]
    public string Page1croad { get; set; }
    [JsonProperty("Page1c/kenphia1")]
    public string Page1ckenphia1 { get; set; }
    [JsonProperty("Page2/room")]
    public string Page2room { get; set; }
    [JsonProperty("Page2/air")]
    public string Page2air { get; set; }
    [JsonProperty("Page2/air_comment")]
    public string Page2air_comment { get; set; }
    [JsonProperty("Page2/clutter")]
    public string Page2clutter { get; set; }
    [JsonProperty("Page2/clutter_opt")]
    public string Page2clutter_opt { get; set; }
    [JsonProperty("Page2/windows")]
    public string Page2windows { get; set; }
    [JsonProperty("Page2/window_bulgar")]
    public string Page2window_bulgar { get; set; }
    [JsonProperty("Page2/doors")]
    public string Page2doors { get; set; }
    [JsonProperty("Page2/roof")]
    public string Page2roof { get; set; }
    [JsonProperty("Page2/shelves")]
    public string Page2shelves { get; set; }
    [JsonProperty("Page2/shelves_comments")]
    public string Page2shelves_comments { get; set; }
    [JsonProperty("Page2/lab_access")]
    public string Page2lab_access { get; set; }
    [JsonProperty("Page2/space")]
    public string Page2space { get; set; }
    [JsonProperty("Page2/length")]
    public string Page2length { get; set; }
    [JsonProperty("Page2/width")]
    public string Page2width { get; set; }
    [JsonProperty("Page2/space_comment")]
    public string Page2space_comment { get; set; }
    [JsonProperty("Page2/chairs")]
    public string Page2chairs { get; set; }
    [JsonProperty("Page2/cover")]
    public string Page2cover { get; set; }
    [JsonProperty("Page2/cover_type")]
    public string Page2cover_type { get; set; }
    [JsonProperty("Page2/electricity")]
    public string Page2electricity { get; set; }
    [JsonProperty("Page2/power")]
    public string Page2power { get; set; }
    [JsonProperty("Page2/electricity_blackout")]
    public string Page2electricity_blackout { get; set; }
    [JsonProperty("Page2/blackout_duration")]
    public string Page2blackout_duration { get; set; }
    [JsonProperty("Page2/generator")]
    public string Page2generator { get; set; }
    [JsonProperty("Page2/facility_generator")]
    public string Page2facility_generator { get; set; }
    [JsonProperty("Page2/gen_fuel")]
    public string Page2gen_fuel { get; set; }
    [JsonProperty("Page2/lab_gen")]
    public string Page2lab_gen { get; set; }
    [JsonProperty("Page2/gen_supply")]
    public string Page2gen_supply { get; set; }
    [JsonProperty("Page2/independent_circuit")]
    public string Page2independent_circuit { get; set; }
    [JsonProperty("Page2/circuit_type")]
    public string Page2circuit_type { get; set; }
    [JsonProperty("Page2/sockets")]
    public string Page2sockets { get; set; }
    [JsonProperty("Page2/func_bulb")]
    public string Page2func_bulb { get; set; }
    [JsonProperty("Page2a/reception_area")]
    public string Page2areception_area { get; set; }
    [JsonProperty("Page2a/reception_area1")]
    public string Page2areception_area1 { get; set; }
    [JsonProperty("Page2a/reception_clutterfree")]
    public string Page2areception_clutterfree { get; set; }
    [JsonProperty("Page2a/documented_sample_system")]
    public string Page2adocumented_sample_system { get; set; }
    [JsonProperty("Page2a/doc_samples")]
    public string Page2adoc_samples { get; set; }
    [JsonProperty("Page2a/lims_db_link")]
    public string Page2alims_db_link { get; set; }
    [JsonProperty("Page2a/samp_integrity")]
    public string Page2asamp_integrity { get; set; }
    [JsonProperty("Page2a/funct_freezer")]
    public string Page2afunct_freezer { get; set; }
    [JsonProperty("Page2a/funct_freezer1")]
    public string Page2afunct_freezer1 { get; set; }
    [JsonProperty("Page2a/funct_fridge")]
    public string Page2afunct_fridge { get; set; }
    [JsonProperty("Page2a/fridge_funct")]
    public string Page2afridge_funct { get; set; }
    [JsonProperty("Page2a/cont_plan")]
    public string Page2acont_plan { get; set; }
    [JsonProperty("Page2a/cont_plan1")]
    public string Page2acont_plan1 { get; set; }
    [JsonProperty("Page2a/fnct_centrifuge")]
    public string Page2afnct_centrifuge { get; set; }
    [JsonProperty("Page2a/tap_water")]
    public string Page2atap_water { get; set; }
    [JsonProperty("Page2a/transit_document")]
    public string Page2atransit_document { get; set; }
    [JsonProperty("Page2a/leaks")]
    public string Page2aleaks { get; set; }
    [JsonProperty("Page3/geneXpert")]
    public string Page3geneXpert { get; set; }
    [JsonProperty("Page3/xpert_module")]
    public string Page3xpert_module { get; set; }
    [JsonProperty("Page3/funct_modules")]
    public string Page3funct_modules { get; set; }
    [JsonProperty("Page3/Use_GeneXpert")]
    public string Page3Use_GeneXpert { get; set; }
    [JsonProperty("Page3/avg_Xpert")]
    public string Page3avg_Xpert { get; set; }
    [JsonProperty("Page3/avg_Xpert1")]
    public string Page3avg_Xpert1 { get; set; }
    [JsonProperty("Page3/Xpert_contract")]
    public string Page3Xpert_contract { get; set; }
    [JsonProperty("Page3/Xpert_service")]
    public string Page3Xpert_service { get; set; }
    [JsonProperty("Page3/Xpert_time")]
    public string Page3Xpert_time { get; set; }
    [JsonProperty("Page3/Xpert_staff")]
    public string Page3Xpert_staff { get; set; }
    [JsonProperty("Page3/Xpert_QC")]
    public string Page3Xpert_QC { get; set; }
    [JsonProperty("Page3/Xpert1_QC")]
    public string Page3Xpert1_QC { get; set; }
    [JsonProperty("Page3/EQA")]
    public string Page3EQA { get; set; }
    [JsonProperty("Page3/EQA_TIME")]
    public string Page3EQA_TIME { get; set; }
    [JsonProperty("Page3/eqa_rst")]
    public string Page3eqa_rst { get; set; }
    [JsonProperty("Page3/Xpert_add")]
    public string Page3Xpert_add { get; set; }
    [JsonProperty("Page3a/pima_machines")]
    public string Page3apima_machines { get; set; }
    [JsonProperty("Page3a/func_pima")]
    public string Page3afunc_pima { get; set; }
    [JsonProperty("Page3a/CRAG")]
    public string Page3aCRAG { get; set; }
    [JsonProperty("Page3b/Hep_B")]
    public string Page3bHep_B { get; set; }
    [JsonProperty("Page3b/testkit")]
    public string Page3btestkit { get; set; }
    [JsonProperty("Page3b/Hep_C")]
    public string Page3bHep_C { get; set; }
    [JsonProperty("Page3b/syp")]
    public string Page3bsyp { get; set; }
    [JsonProperty("Page3b/syp_kit")]
    public string Page3bsyp_kit { get; set; }
    [JsonProperty("Page4/ISO_certified")]
    public string Page4ISO_certified { get; set; }
    [JsonProperty("Page4/sop")]
    public string Page4sop { get; set; }
    [JsonProperty("Page4/staff_trainedSOP")]
    public string Page4staff_trainedSOP { get; set; }
    [JsonProperty("Page4/prof")]
    public string Page4prof { get; set; }
    [JsonProperty("Page5/add_room")]
    public string Page5add_room { get; set; }
    [JsonProperty("Page5/proximity_room")]
    public string Page5proximity_room { get; set; }
    [JsonProperty("Page5/proposed_lab")]
    public string Page5proposed_lab { get; set; }
    [JsonProperty("Page5/length_store")]
    public string Page5length_store { get; set; }
    [JsonProperty("Page5/width_store")]
    public string Page5width_store { get; set; }
    [JsonProperty("Page5/access_store")]
    public string Page5access_store { get; set; }
    [JsonProperty("Page5/store_secure")]
    public string Page5store_secure { get; set; }
    [JsonProperty("Page5/air_c_s")]
    public string Page5air_c_s { get; set; }
    [JsonProperty("Page5/air_c_s1")]
    public string Page5air_c_s1 { get; set; }
    [JsonProperty("Page5/sun")]
    public string Page5sun { get; set; }
    [JsonProperty("Page5/store_comments")]
    public string Page5store_comments { get; set; }
    [JsonProperty("Page5/store_image1")]
    public string Page5store_image1 { get; set; }
    [JsonProperty("Page5/store_image2")]
    public string Page5store_image2 { get; set; }
    [JsonProperty("Page5/store_image3")]
    public string Page5store_image3 { get; set; }
    [JsonProperty("Page6/waste_mgt")]
    public string Page6waste_mgt { get; set; }
    [JsonProperty("Page6/sharp_waste")]
    public string Page6sharp_waste { get; set; }
    [JsonProperty("Page6/bio_hazard")]
    public string Page6bio_hazard { get; set; }
    [JsonProperty("Page6/other_waste")]
    public string Page6other_waste { get; set; }
    [JsonProperty("Page6/liquid_waste")]
    public string Page6liquid_waste { get; set; }
    [JsonProperty("Page6/waste_add")]
    public string Page6waste_add { get; set; }
    [JsonProperty("Page6/waste_image")]
    public string Page6waste_image { get; set; }
    [JsonProperty("Page6/waste_image1")]
    public string Page6waste_image1 { get; set; }
    [JsonProperty("Page6/dispose_sharps")]
    public string Page6dispose_sharps { get; set; }
    [JsonProperty("Page6/dispose_solids")]
    public string Page6dispose_solids { get; set; }
    [JsonProperty("Page6/biohazard_liq")]
    public string Page6biohazard_liq { get; set; }
    [JsonProperty("Page6/non_biohazard_waste")]
    public string Page6non_biohazard_waste { get; set; }
    [JsonProperty("Page6/wate_gen")]
    public string Page6wate_gen { get; set; }
    [JsonProperty("Page6/wate_gen1")]
    public string Page6wate_gen1 { get; set; }
    [JsonProperty("Page6/disposal")]
    public string Page6disposal { get; set; }
    [JsonProperty("Page6/autoclaves")]
    public string Page6autoclaves { get; set; }
    [JsonProperty("Page6/waste_contigency")]
    public string Page6waste_contigency { get; set; }
    [JsonProperty("Page6/waste_contigency1")]
    public string Page6waste_contigency1 { get; set; }
    [JsonProperty("Page6/waste_dsp_site")]
    public string Page6waste_dsp_site { get; set; }
    [JsonProperty("Page6/waste_job_aid")]
    public string Page6waste_job_aid { get; set; }
    [JsonProperty("Page7/parking")]
    public string Page7parking { get; set; }
    [JsonProperty("Page7/parking1")]
    public string Page7parking1 { get; set; }
    [JsonProperty("Page7/parking_image")]
    public string Page7parking_image { get; set; }
    [JsonProperty("Page7/freezer_area")]
    public string Page7freezer_area { get; set; }
    [JsonProperty("Page7/freezer_area1")]
    public string Page7freezer_area1 { get; set; }
    [JsonProperty("Page7/extra_space")]
    public string Page7extra_space { get; set; }
    [JsonProperty("Page7/parking2")]
    public string Page7parking2 { get; set; }
    [JsonProperty("Page7/parking3")]
    public string Page7parking3 { get; set; }
    [JsonProperty("Page8/guards")]
    public string Page8guards { get; set; }
    [JsonProperty("Page8/security24")]
    public string Page8security24 { get; set; }
    [JsonProperty("Page8/security_type")]
    public string Page8security_type { get; set; }
    [JsonProperty("Page8/security_out")]
    public string Page8security_out { get; set; }
    [JsonProperty("Page8/lab_lock")]
    public string Page8lab_lock { get; set; }
    [JsonProperty("Page8/lab_lock_w")]
    public string Page8lab_lock_w { get; set; }
    [JsonProperty("Page8/lock")]
    public string Page8lock { get; set; }
    [JsonProperty("Page8/lock_image")]
    public string Page8lock_image { get; set; }
    [JsonProperty("Page8/safety")]
    public string Page8safety { get; set; }
    [JsonProperty("Page8/fire_safety")]
    public string Page8fire_safety { get; set; }
    [JsonProperty("Page8/fire_equip")]
    public string Page8fire_equip { get; set; }
    [JsonProperty("Page8/additional_safety")]
    public string Page8additional_safety { get; set; }
    [JsonProperty("Page8/safety_equip")]
    public string Page8safety_equip { get; set; }
    [JsonProperty("Page9/mobile_network")]
    public string Page9mobile_network { get; set; }
    [JsonProperty("Page9/mobile_network1")]
    public string Page9mobile_network1 { get; set; }
    [JsonProperty("Page9/network4G")]
    public string Page9network4G { get; set; }
    [JsonProperty("Page10/sign")]
    public string Page10sign { get; set; }
    [JsonProperty("Page11/assessor_conclusion")]
    public string Page11assessor_conclusion { get; set; }
    [JsonProperty("Page11/assesor_remarks")]
    public string Page11assesor_remarks { get; set; }
    public string __version__ { get; set; }
    [JsonProperty("meta/instanceID")]
    public string metainstanceID { get; set; }
    public string _xform_id_string { get; set; }
    public string _uuid { get; set; }
    public List<Attachment> _attachments { get; set; }
    public string _status { get; set; }
    public List<double> _geolocation { get; set; }
    public DateTime _submission_time { get; set; }
    public List<object> _tags { get; set; }
    public List<object> _notes { get; set; }
    public ValidationStatus _validation_status { get; set; }
    public string _submitted_by { get; set; }
    public double TotalScore { get; set; }
}
public class ValidationStatus
{
}

